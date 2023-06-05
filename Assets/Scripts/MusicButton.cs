using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    private BGMusic music;
    public Button musicToggleButton;
    public Sprite mutedSprite;
    public Sprite unmutedSprite;

    void Start()
    {
        music = GameObject.FindObjectOfType<BGMusic>();
        UpdateIcon();
    }

    public void PauseMusic()
    {
        music.ToggleSound();
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            music.UnmuteAudio();
            musicToggleButton.GetComponent<Image>().sprite = mutedSprite;
        }
        else
        {
            music.MuteAudio();
            musicToggleButton.GetComponent<Image>().sprite = unmutedSprite;
        }
    }
}
