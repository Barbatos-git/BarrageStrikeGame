using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 targetPosition;
    private float minX, maxX, minY, maxY;
    private CircleCollider2D bossCircleCollider;
    private Vector2 offset;
    private bool isAlive = true;
    public bool isMovable = false;
    // Start is called before the first frame update
    void Start()
    {
        bossCircleCollider = GetComponent<CircleCollider2D>();
        SetRandomTargetPosition();
        if (bossCircleCollider != null)
        {
            offset = bossCircleCollider.offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && isMovable)
        {
            MoveToTarget();
        }
    }

    void SetRandomTargetPosition() 
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWideth = cameraHeight * mainCamera.aspect;
        minX = mainCamera.transform.position.x - cameraWideth / 2 + bossCircleCollider.radius;
        maxX = mainCamera.transform.position.x + cameraWideth / 2 - bossCircleCollider.radius;
        minY = mainCamera.transform.position.y + cameraHeight / 6 + bossCircleCollider.radius - 1.5f;
        maxY = mainCamera.transform.position.y + cameraHeight / 2 - bossCircleCollider.radius - 1.5f;

        float targetX = Random.Range(minX, maxX);
        float targetY = Random.Range(minY, maxY);

        targetPosition = new Vector3(targetX, targetY, transform.position.z);
    }

    private void MoveToTarget()
    {
        if (Vector3.Distance(transform.position + (Vector3)offset, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
        transform.position = Vector3.MoveTowards(transform.position + (Vector3)offset, targetPosition, moveSpeed * Time.deltaTime) - (Vector3)offset;
    }

    public void StopMovement()
    {
        isAlive = false;
    }
}
