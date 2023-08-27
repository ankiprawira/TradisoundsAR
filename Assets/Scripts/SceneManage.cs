using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public string urlLink;

    public void LoadToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenLink()
    {
        Application.OpenURL(urlLink);
    }

    public void SetTimeScaleTo1()
    {
        Time.timeScale = 1;
    }
    public void SetTimeScaleTo0()
    {
        Time.timeScale = 0;
    }
}
