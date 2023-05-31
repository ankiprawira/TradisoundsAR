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

    // public Sprite crossSprite;

    // private Image additionalSpriteImage;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindObjectOfType<BGMusic>();
        // additionalSpriteImage = musicToggleButton.gameObject.AddComponent<Image>();
        UpdateIcon();
    }

    // Update is called once per frame
    void Update() { }

    public void PauseMusic()
    {
        music.ToggleSound();
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            musicToggleButton.GetComponent<Image>().sprite = mutedSprite;
            // additionalSpriteImage.sprite = crossSprite; // Set the additional sprite
            // additionalSpriteImage.enabled = true;
        }
        else
        {
            AudioListener.volume = 0;
            musicToggleButton.GetComponent<Image>().sprite = unmutedSprite;
            // additionalSpriteImage.enabled = false;
        }
    }
}
