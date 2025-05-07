using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject settingsMenuUI;
    private bool isMenuOpen = false;
    public string nextSceneName = "";
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        settingsMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        if (isMenuOpen)
        {
            return;
        }
        isMenuOpen = true;
        settingsMenuUI.SetActive(true);
        Time.timeScale = 0f;
        audioSource.Pause();
    }

    public void ResumeGame()
    {
        if (!isMenuOpen)
        {
            return;
        }
        isMenuOpen = false;
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        audioSource.Play();
    }

    public void LoadPreviousScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }
}
