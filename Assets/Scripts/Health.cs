using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    private bool hasTriggeredDeathEvent = false;

    // i-frame
    [SerializeField] float iFrameDuration = 2.5f;
    bool iFrameMode = false;

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

    public void SetHealth(int newHealth, bool bypassNonpositiveHealth = false)
    {
        if (!bypassNonpositiveHealth && newHealth <= 0) {
            Debug.LogWarning("New Health is nonpositive: " + newHealth + "\n Exiting SetHealth function.");
            return;
        }

        health = newHealth;
    }

    public void Damage(float damage, bool bypassNegativeDamage = false)
    {
        // Edge case: damage is negative
        if (!bypassNegativeDamage && damage < 0)
        {
            Debug.LogWarning("Damage is negative: " + damage + "\n Exiting Damage function.");
            return;
        }

        // Modify health
        health -= damage;
        TakeDamageEvent(health);
        
        if (health <= 0f && !hasTriggeredDeathEvent)
        {
            DeathEvent();
            hasTriggeredDeathEvent = true;
            // TODO: change sprite to death animation and disable player interaction system
            //Destroy(gameObject.transform.parent.gameObject);
        }

        BeginIFrame();
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






    // This is the iFrame:

    IEnumerator IFrameTime()
    {
        iFrameMode = false;
        yield return new WaitForSeconds(iFrameDuration);
    }

    void BeginIFrame()
    {
        iFrameMode = true;
        StartCoroutine(IFrameTime());
    }
}