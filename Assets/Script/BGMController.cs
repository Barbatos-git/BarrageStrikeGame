using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMController : MonoBehaviour
{
    public AudioSource audioSource;
    public Button bgmButton;
    public Sprite bgmOnSprite;
    public Sprite bgmOffSprite;
    private bool isBGMOn = true;
    // Start is called before the first frame update
    void Start()
    {
        bgmButton.onClick.AddListener(ToggleBGM);

        UpdateButtonImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleBGM()
    {
        isBGMOn = !isBGMOn;
        if (isBGMOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

        UpdateButtonImage();
    }

    void UpdateButtonImage()
    {
        if (isBGMOn)
        {
            bgmButton.image.sprite = bgmOnSprite;
        }
        else
        {
            bgmButton.image.sprite = bgmOffSprite;
        }
    }
}
