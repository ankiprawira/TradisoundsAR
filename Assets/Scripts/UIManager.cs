using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable()]
public struct UIManagerParameters
{
    [Header("Answers Options")]
    [SerializeField]
    float margins;
    public float Margins
    {
        get { return margins; }
    }

    [Header("Resolution Screen Options")]
    [SerializeField]
    Color correctBGColor;
    public Color CorrectBGColor
    {
        get { return correctBGColor; }
    }

    [SerializeField]
    Color incorrectBGColor;
    public Color IncorrectBGColor
    {
        get { return incorrectBGColor; }
    }

    [SerializeField]
    Color finalBGColor;
    public Color FinalBGColor
    {
        get { return finalBGColor; }
    }
}

[Serializable()]
public struct UIElements
{
    [SerializeField]
    RectTransform answersContentArea;
    public RectTransform AnswersContentArea
    {
        get { return answersContentArea; }
    }

    [SerializeField]
    TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject
    {
        get { return questionInfoTextObject; }
    }

    [SerializeField]
    TextMeshProUGUI countDownGame;
    public TextMeshProUGUI CountDownGame
    {
        get { return countDownGame; }
    }

    [SerializeField]
    TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText
    {
        get { return scoreText; }
    }

    [Space]
    [SerializeField]
    Animator resolutionScreenAnimator;
    public Animator ResolutionScreenAnimator
    {
        get { return resolutionScreenAnimator; }
    }

    [SerializeField]
    Image resolutionBG;
    public Image ResolutionBG
    {
        get { return resolutionBG; }
    }

    [SerializeField]
    TextMeshProUGUI resolutionStateInfoText;
    public TextMeshProUGUI ResolutionStateInfoText
    {
        get { return resolutionStateInfoText; }
    }

    [SerializeField]
    TextMeshProUGUI resolutionScoreText;
    public TextMeshProUGUI ResolutionScoreText
    {
        get { return resolutionScoreText; }
    }

    [Space]
    [SerializeField]
    TextMeshProUGUI highScoreText;
    public TextMeshProUGUI HighScoretext
    {
        get { return highScoreText; }
    }

    [SerializeField]
    CanvasGroup mainCanvasGroup;
    public CanvasGroup MainCanvasGroup
    {
        get { return mainCanvasGroup; }
    }

    [SerializeField]
    RectTransform finishUIElements;
    public RectTransform FinishUIElements
    {
        get { return finishUIElements; }
    }

    [SerializeField]
    Button nextButton;
    public Button NextButton
    {
        get { return nextButton; }
    }

    [SerializeField]
    Image questionContentArea;
    public Image QuestionContentArea
    {
        get { return questionContentArea; }
    }

    [SerializeField]
    Image countdownOverlay;
    public Image CountdownOverlay
    {
        get { return countdownOverlay; }
    }
}

public class UIManager : MonoBehaviour
{
    public enum ResolutionScreenType
    {
        Correct,
        Incorrect,
        Finished
    }

    [Header("References")]
    [SerializeField]
    GameEvents events;

    [Header("UI Elements (Prefabs)")]
    [SerializeField]
    AnswerData answerPrefab;

    [SerializeField]
    UIElements uIElements;

    [Space]
    [SerializeField]
    UIManagerParameters parameters;

    List<AnswerData> currentAnswer = new List<AnswerData>();
    private int resStateParaHash = 0;

    private IEnumerator IE_DisplayTimedResolution;

    void OnEnable()
    {
        events.UpdateQuestionUI += UpdateQuestionUI;
        events.DisplayResolutionScreen += DisplayResolution;
        events.ScoreUpdated += UpdateScoreUI;
    }

    void OnDisable()
    {
        events.UpdateQuestionUI -= UpdateQuestionUI;
        events.DisplayResolutionScreen -= DisplayResolution;
        events.ScoreUpdated -= UpdateScoreUI;
    }

    void Start()
    {
        Time.timeScale = 0; //to pause for countdown
        StartCoroutine(CountDownToStart());
        UpdateScoreUI();
        resStateParaHash = Animator.StringToHash("ScreenState");
    }

    void UpdateQuestionUI(Question question)
    {
        uIElements.QuestionInfoTextObject.text = question.Info;
        CreateAnswers(question);
    }

    void DisplayResolution(ResolutionScreenType type, int score)
    {
        UpdateResolutionUI(type, score);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 2);
        uIElements.MainCanvasGroup.blocksRaycasts = false;

        if (type != ResolutionScreenType.Finished)
        {
            if (IE_DisplayTimedResolution != null)
            {
                StopCoroutine(IE_DisplayTimedResolution);
            }
            IE_DisplayTimedResolution = DisplayTimedResolution();
            StartCoroutine(IE_DisplayTimedResolution);
        }
    }

    IEnumerator DisplayTimedResolution()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 1);
        uIElements.MainCanvasGroup.blocksRaycasts = true;
    }

    void UpdateResolutionUI(ResolutionScreenType type, int score)
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
        switch (type)
        {
            case ResolutionScreenType.Correct:
                uIElements.ResolutionBG.color = parameters.CorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "BENAR!";
                uIElements.ResolutionScoreText.text = "+" + score;
                break;
            case ResolutionScreenType.Incorrect:
                uIElements.ResolutionBG.color = parameters.IncorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "SALAH!";
                uIElements.ResolutionScoreText.text = "-" + score;
                break;
            case ResolutionScreenType.Finished:
                uIElements.ResolutionBG.color = parameters.FinalBGColor;
                uIElements.ResolutionStateInfoText.text = "SKOR AKHIR";
                //uIElements.ResolutionScoreText.text = events.CurrentFinalScore.ToString();
                StartCoroutine(CalculateScore());
                uIElements.FinishUIElements.gameObject.SetActive(true);
                uIElements.HighScoretext.gameObject.SetActive(true);
                uIElements.HighScoretext.text =
                    (
                        (highscore > events.StartupHighscore)
                            ? "<color=yellow>New </color>"
                            : string.Empty
                    )
                    + "High Score: "
                    + highscore;
                break;
        }
    }

    IEnumerator CalculateScore()
    {
        var scoreValue = 0;
        if (events.CurrentFinalScore < 0)
        {
            while (scoreValue > events.CurrentFinalScore)
            {
                scoreValue--;
                uIElements.ResolutionScoreText.text = scoreValue.ToString();

                yield return null;
            }
        }
        while (scoreValue < events.CurrentFinalScore)
        {
            scoreValue++;
            uIElements.ResolutionScoreText.text = scoreValue.ToString();

            yield return null;
        }
    }

    void CreateAnswers(Question question)
    {
        EraseAnswers();

        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = (AnswerData)Instantiate(
                answerPrefab,
                uIElements.AnswersContentArea
            );
            newAnswer.UpdateData(question.Answers[i].Info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);

            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            uIElements.AnswersContentArea.sizeDelta = new Vector2(
                uIElements.AnswersContentArea.sizeDelta.x,
                offset * -1
            );

            currentAnswer.Add(newAnswer);
        }
    }

    void EraseAnswers()
    {
        foreach (var answer in currentAnswer)
        {
            Destroy(answer.gameObject);
        }
        currentAnswer.Clear();
    }

    void UpdateScoreUI()
    {
        uIElements.ScoreText.text = "SKOR: " + events.CurrentFinalScore;
    }

    public int countdownTime;

    IEnumerator CountDownToStart()
    {
        while (countdownTime > 0)
        {
            uIElements.CountDownGame.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);

            countdownTime--;
        }
        uIElements.CountDownGame.text = "MULAI!";
        yield return new WaitForSecondsRealtime(1f);
        EnableQUizObject(false); //
        Time.timeScale = 1;
    }

    void EnableQUizObject(bool statement)
    {
        uIElements.CountDownGame.gameObject.SetActive(statement);
        uIElements.CountdownOverlay.gameObject.SetActive(statement);
    }
}
