using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyExplosionAndHpController : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private EnemyController enemyController;
    private bool hasDied = false;
    [SerializeField] private float fadeDuration = 1f;
    private SpriteRenderer spriteRenderer;
    private bool isFading = false;
    public int damage = 1;
    public float maxHp = 1000f;
    [SerializeField] private float currentHp;
    public UnityEvent isCollision;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0f && !hasDied)
        {
            OnEnemyDeath();
        }
    }

    public void OnEnemyDeath()
    {
        if (hasDied) return;
        hasDied = true;

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        if (enemyController != null)
        {
            enemyController.StopMovement();
        }
        if (!isFading)
        {
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(1f);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasDied)
        {
            PlayerHpController playerHp = collision.gameObject.GetComponent<PlayerHpController>();
            if (playerHp != null)
            {
                playerHp.TakeDamage(damage);
                isCollision?.Invoke();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (hasDied)
        {
            return;
        }
        currentHp -= damage;

        if (currentHp <= 0f)
        {
            enemyController.Die();
        }
    }

    public void Initialize()
    {
        currentHp = maxHp;
        hasDied = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
