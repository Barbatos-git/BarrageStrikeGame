using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bulletCount = 20;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (360f / bulletCount);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * speed;
        }
    }
}
