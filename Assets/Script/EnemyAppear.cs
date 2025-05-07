using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppear : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform spawnAreaLeft;
    public Transform spawnAreaRight;
    public int initiaEnemyCount = 5;
    public int secondEnemyCount = 3;
    private List<GameObject> activeEnemies = new List<GameObject>();
    public Vector3 enemyPoint;
    private InvincibleAndFlashController playerInvincibleController;
    private GameObject enemy;
    private bool isBossAppear = false;
    private bool isSecond = false;
    private BossAppear bossAppear;
    // Start is called before the first frame update
    void Start()
    {
        bossAppear = FindObjectOfType<BossAppear>();
        playerInvincibleController = FindObjectOfType<InvincibleAndFlashController>();
        StartCoroutine(SpawnEnemies(enemyPrefabs[0], initiaEnemyCount));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            bool spawnFromLeft = Random.value > 0.5f;
            Transform spawnArea = spawnFromLeft ? spawnAreaLeft : spawnAreaRight;

            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localPosition.x / 2, spawnArea.position.x + spawnArea.localPosition.x / 2),
                spawnArea.position.y,
                spawnArea.position.z
                );

            enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.SetTargetPosition(enemyPoint);
                enemyController.onEnemyDeath += OnEnemyDeath;
            }

            EnemyExplosionAndHpController enemyExplosionAndHp = enemy.GetComponent<EnemyExplosionAndHpController>();
            if (enemyExplosionAndHp != null && playerInvincibleController != null)
            {
                enemyExplosionAndHp.isCollision.AddListener(playerInvincibleController.CollisionWithEnemy);
                enemyExplosionAndHp.Initialize();
            }

            activeEnemies.Add(enemy);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }

        if (activeEnemies.Count == 0 && !isSecond)
        {
            StartCoroutine(SpawnSceondWave());
        }
        if (isBossAppear && activeEnemies.Count == 0 && isSecond)
        {
            bossAppear.MoveBoss();
            BossBulletController bossBulletController = FindObjectOfType<BossBulletController>();
            bossBulletController.isMovable = true;
        }
    }

    IEnumerator SpawnSceondWave()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnEnemies(enemyPrefabs[1], secondEnemyCount));
        isBossAppear = true;
        isSecond = true;
    }
}
