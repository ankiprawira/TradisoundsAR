// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.Audio;

using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isARScene = false;

    private static BGMusic instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene is the "AR" scene
        if (scene.name == "AR")
        {
            isARScene = true;
            PauseAudio();
        }
        else if (scene.name == "Main Menu")
        {
            isARScene = false;
            ResumeAudio();
        }
    }

    private void PauseAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    private void ResumeAudio()
    {
        if (!audioSource.isPlaying && !isARScene)
        {
            audioSource.UnPause();
        }
    }

    public void MuteAudio()
{
    audioSource.volume = 0;
}

public void UnmuteAudio()
{
    audioSource.volume = 1;
}


    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }
    }
}
