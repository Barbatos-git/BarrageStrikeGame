using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerExplosionController : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float fadeDuration = 1f;
    private SpriteRenderer spriteRenderer;
    private bool isFading = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerHpController playerHp = FindObjectOfType<PlayerHpController>();
        if (playerHp != null)
        {
            playerHp.onPlayerDeath.AddListener(OnPlayerDeath);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnPlayerDeath()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        if (!isFading)
        {
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        isFading = true;
        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        Destroy(gameObject);
    }
}
