using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth; // maximum health of the player
    private float health;
    private bool dead = false;

    private void Start() 
    {
        health = maxHealth;
    }

    public void ResetHealth() 
    {
        health = maxHealth;
    }

    public float GetMaxHealth() 
    {
        return maxHealth;
    }

    public float GetHealth() 
    {
        return health;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public void Damage(float damage)
    {
        // Edge case: damage is negative
        if (damage < 0)
        {
            Debug.LogWarning("Damage is negative: " + damage + "\n Exiting Damage function.");
            return;
        }

        // Modify health
        health -= damage;
        if (TakeDamageEvent != null)
        {
            TakeDamageEvent(health);
        }
        if (health <= 0f && !dead)
        {
            if (DeathEvent != null)
            {
                DeathEvent();
            }
            dead = true;
            // TODO: change sprite to death animation and disable player interaction system
            //Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void Heal(float heal)
    {
        if (heal < 0) 
        {
            Debug.LogWarning("Healing is negative: " + heal + "\n Exiting Heal function.");
            return;
        }

        // Modify health
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }


    public bool Death() 
    {
        return health <= 0;
    }

    // events
    public delegate void TakeDamageEventHandler(float newHealth);
    public event TakeDamageEventHandler TakeDamageEvent;

    public delegate void DeathEventHandler();
    public event DeathEventHandler DeathEvent;
}   
