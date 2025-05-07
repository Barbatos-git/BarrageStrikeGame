using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHpBarController : MonoBehaviour
{
    public Image hpBarFill;
    public float maxHp = 1300f;
    [SerializeField]private float currentHp;
    public GameObject bossHpBar;
    public UnityEvent onHpZero;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        //bossHpBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHpBar()
    {
        if (hpBarFill != null)
        {
            hpBarFill.fillAmount = currentHp / maxHp;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        //Debug.Log(currentHp);
        UpdateHpBar();
        if (currentHp <= 0f)
        {
            onHpZero?.Invoke();
        }
    }
}
