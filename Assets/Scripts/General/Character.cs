using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本参数")]
    public int currentHealth;
    public int maxHealth;

    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        invulnerableCounter -= Time.deltaTime;
        if(invulnerableCounter <= 0.0f)
        {
            invulnerable = false;
        }
    }

    public void TakeDamage(Attack attack)
    {
        if (invulnerable)
            return;

        if(currentHealth - attack.attackDamage > 0)
        {
            currentHealth -= attack.attackDamage;
            TirrgerInvulnerable();
            OnTakeDamage?.Invoke(attack.transform);
        }
        else
        {
            currentHealth = 0;
            //执行死亡
            OnDead?.Invoke();
        }

    }

    public void TirrgerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
