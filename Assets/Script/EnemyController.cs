using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector3 targetPosition;
    private bool reachedCenter = false;
    public Action<GameObject> onEnemyDeath;
    private Rigidbody2D rb;
    private bool isAlive = true;
    private EnemyExplosionAndHpController enemyExplosionAndHp;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyExplosionAndHp = GetComponent<EnemyExplosionAndHpController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedCenter && isAlive)
        {
            MoveToTarget();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            reachedCenter = true;
            StartCoroutine(RandomMovement());
        }
    }

    IEnumerator RandomMovement()
    { 
        while (isAlive)
        {
            Vector3 randomPosition = new Vector3(
                targetPosition.x + UnityEngine.Random.Range(-8f, 8f),
                targetPosition.y + UnityEngine.Random.Range(-5f, 5f),
                transform.position.z
                );

            while (Vector3.Distance(transform.position, randomPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, randomPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        }
    }

    public void Die()
    {
        if (!isAlive)
        {
            return;
        }
        isAlive = false;
        rb.linearVelocity = Vector2.zero;
        onEnemyDeath?.Invoke(gameObject);

        if (enemyExplosionAndHp != null)
        {
            enemyExplosionAndHp.OnEnemyDeath();
        }
    }

    public void StopMovement()
    {
        isAlive = false;
        rb.linearVelocity = Vector2.zero;
    }
}
