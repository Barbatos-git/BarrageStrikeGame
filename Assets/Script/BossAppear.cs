using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAppear : MonoBehaviour
{
    public Transform boss;
    public float appearSpeed = 3f;
    public Vector3 targetPosition;
    public GameObject bossHpBar;
    private BossController bossController;
    public UnityEvent bossHpActive;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPosition = new Vector3(0, 30f, 0);
        boss.position = startPosition;

        bossController = FindObjectOfType<BossController>();
        if (bossController != null)
        {
            bossController.isMovable = false;
        }

        //StartCoroutine(MoveBossToPosition());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MoveBossToPosition()
    {
        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(boss.position, targetPosition) > 0.1f)
        {
            boss.position = Vector3.MoveTowards(boss.position, targetPosition, appearSpeed * Time.deltaTime);
            yield return null;
        }

        boss.position = targetPosition;
        bossHpBar.SetActive(true);
        bossHpActive?.Invoke();

        if (bossController != null)
        {
            bossController.isMovable = true;
        }
    }

    public void MoveBoss()
    {
        StartCoroutine(MoveBossToPosition());
    }
}
