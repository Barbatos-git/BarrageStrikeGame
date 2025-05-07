using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1BulletController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed = 2f;
    private InvincibleAndFlashController playerInvincibleController;
    // Start is called before the first frame update
    void Start()
    {
        playerInvincibleController = FindObjectOfType<InvincibleAndFlashController>();
        StartCoroutine(FireBarrage());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FireBarrage()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(RingBullt());
    }

    IEnumerator RingBullt()
    {
        int bulletCount = 8;
        float radius = 0.5f;
        while (true)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * (360f / bulletCount);
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                Vector3 spawnPosition = transform.position + (Vector3)(direction * radius);
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * speed;

                BarrageController barrage = bullet.GetComponent<BarrageController>();
                if (barrage != null && playerInvincibleController != null)
                {
                    barrage.isCollision.AddListener(playerInvincibleController.CollisionWithBarrage);
                }
            }
            yield return new WaitForSeconds(4f);
        }
    }
}
