        using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI bossHpText;
    public TextMeshProUGUI gameTimeText;
    public TextMeshProUGUI playerHpText;
    public TextMeshProUGUI scoreText;

    private float displayScore = 0;
    private float targetScore;
    // Start is called before the first frame update
    void Start()
    {
        float bossHp = GameDataManager.Instance.bossHp;
        float gameTime = GameDataManager.Instance.gameTime;
        float playerHp = GameDataManager.Instance.playerHp;
        float maxPlayerHp = GameDataManager.Instance.maxPlayerHp;
        SetUp(bossHp, gameTime, playerHp, maxPlayerHp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(float bossHp, float gameTime, float playerHp, float maxPlayerHp)
    {
        bossHpText.text = bossHp.ToString("F0");

        //gameTimeText.text = gameTime.ToString("F1") + "s";

        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        gameTimeText.text = $"{minutes}:{seconds.ToString("D2")}s";

        playerHpText.text = playerHp + "/" + maxPlayerHp;

        float timeWeight = 0.6f;
        float playerHpWeight = 0.4f;
        float timeScore = Mathf.Clamp(bossHp - gameTime * 10, 0, bossHp);
        float hpScore = (playerHp / maxPlayerHp) * bossHp;

        targetScore = bossHp + timeScore * timeWeight + hpScore * playerHpWeight;
        StartCoroutine(AnimateScore());
    }

    IEnumerator AnimateScore()
    {
        while (displayScore < targetScore)
        {
            displayScore += Time.deltaTime * 20000;
            scoreText.text = Mathf.FloorToInt(displayScore).ToString();
            yield return null;
        }

        displayScore = targetScore;
        scoreText.text = Mathf.FloorToInt(displayScore).ToString();
    }
}
