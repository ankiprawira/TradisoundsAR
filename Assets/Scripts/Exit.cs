using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    GameObject quitPanel;

    void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {
            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitConfirmation();
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Aplikasi Keluar");
    }

    public void ExitConfirmation()
    {
        quitPanel.SetActive(true);
    }

    public void CloseExitConfirmation()
    {
        quitPanel.SetActive(false);
    }
}
