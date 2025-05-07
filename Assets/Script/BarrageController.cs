using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BarrageController : MonoBehaviour
{
    private Camera mainCamera;
    public int damage = 1;
    private BossHpBarController hpBarController;
    private bool hasDied = false;
    public UnityEvent isCollision;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        hpBarController = FindObjectOfType<BossHpBarController>();
        if (hpBarController != null)
        {
            //hpBarController.onHpZero.RemoveListener(OnBossDeath);
            hpBarController.onHpZero.AddListener(OnBossDeath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (IsOutOfScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutOfScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        return screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasDied)
        {
            PlayerHpController playerHp = collision.gameObject.GetComponent<PlayerHpController>();
            if (playerHp != null)
            {
                playerHp.TakeDamage(damage);
                isCollision?.Invoke();
            }
            Destroy(gameObject);
        }
    }

    private void OnBossDeath()
    {
        if (hasDied) { return; }
        hasDied = true;
    }
}
