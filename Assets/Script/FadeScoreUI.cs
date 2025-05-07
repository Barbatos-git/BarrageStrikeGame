using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScoreUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public string nextSceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            TransitionToNextScene();
        }
    }

    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
    }

    void TransitionToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
