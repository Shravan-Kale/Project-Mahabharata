using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float healthAmount;
    
    // local variables
    private float currentHealthAmount;

    private void Awake()
    {
        currentHealthAmount = healthAmount;
    }

    public void GetDamage(float damage)
    {
        Debug.Log("Get dmg");
        currentHealthAmount -= damage;
        if (currentHealthAmount <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Death");
        Destroy(gameObject);
    }
}
