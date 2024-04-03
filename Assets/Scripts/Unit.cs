using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private float health;//체력
    
    [SerializeField]
    private float maxHealth;//최대체력

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = Mathf.Clamp(value, 0f, maxHealth);
            if (health == 0f)
            {
                Kill();
            }
        }
    }
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        if (damage <= 0f)
        {
            return;
        }
        health -= damage;
    }
}
