using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    private bool hasTriggeredDeathEvent = false;

    // invincibility frames
    private float invincibilityDurationSeconds;
    [SerializeField] private bool isInvincible;

    // events
    public delegate void TakeDamageEventHandler(float newHealth);
    public event TakeDamageEventHandler TakeDamageEvent;

    public delegate void DeathEventHandler();
    public event DeathEventHandler DeathEvent;

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

    public bool IsDead() {
        return health <= 0f;
    }

    public bool IsInvincible() {
        return isInvincible;
    }

    public void SetHealth(int newHealth, bool bypassNonpositiveHealth = false)
    {
        if (!bypassNonpositiveHealth && newHealth <= 0) {
            Debug.LogWarning("New Health is nonpositive: " + newHealth + "\n Exiting SetHealth function.");
            return;
        }

        health = newHealth;
    }

    public void Damage(float damage, bool bypassInvincibility = false, bool bypassNegativeDamage = false)
    {
        // Edge case: damage is negative
        if (!bypassNegativeDamage && damage < 0)
        {
            Debug.LogWarning("Damage is negative: " + damage + "\n Exiting Damage function.");
            return;
        }

        // Edge case: is invincible
        if (!bypassInvincibility && isInvincible)
        {
            return;
        }

        // Modify health
        health -= damage;
        TakeDamageEvent?.Invoke(health);


        if (health <= 0f && !hasTriggeredDeathEvent)
        {
            DeathEvent?.Invoke();
            hasTriggeredDeathEvent = true;
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

    IEnumerator InvincibilityRoutine(float invincibilityDurationSeconds)
    {
        while (invincibilityDurationSeconds >= 0f)
        {
            invincibilityDurationSeconds -= Time.deltaTime;
            yield return null;
        }
        isInvincible = false;
    }

    public void SetInvincibility(float invincibilityDurationSeconds, bool bypassNegativeDuration = false, bool bypassAlreadyInvincible = false)
    {
        if (!bypassNegativeDuration) 
        {
            Debug.LogWarning("Invincibility Duration is negative: " + invincibilityDurationSeconds + "\n Exiting SetInvincibility function.");
            return;
        }

        if (!bypassAlreadyInvincible && isInvincible) 
        {
            Debug.LogWarning(this.gameObject + " is already invincible. " + "\n Invicible Duration: " + this.invincibilityDurationSeconds + "\n Exiting SetInvincibility function.");
            return;
        }

        isInvincible = true;
        StartCoroutine(InvincibilityRoutine(invincibilityDurationSeconds));
    }
}