using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
public class BossReceiveHit : MonoBehaviour, IHittable
{
    private Health health; // boss's instance of health
    
    private BossHealthBarUI bossHealthBarUI; // boss's instance of health bar

    void Start()
    {
        health = GetComponent<Health>();
        bossHealthBarUI = GetComponent<BossHealthBarUI>();
    }


    public void TriggerEffects(List<Effect> effects)
    {
        foreach(Effect effect in effects) 
        {
            if (effect.GetEffectType() == Effect.EffectType.Damage)
            {
                ApplyEffectDamage(effect);
            }
        }
    }

    // function to apply effect damage and other additional effects to the boss
    private void ApplyEffectDamage(IEffectDamage effectDamage)
    {
        // deals with the numerical health change of the boss
        Debug.Log("Damage: " + effectDamage.GetDamage()); // output to console for testing
        GetComponent<Health>().Damage(effectDamage.GetDamage());
        
        // deals with the healthbar change of the boss
        bossHealthBarUI.Damage(effectDamage.GetDamage(), health.GetMaxHealth());
        health.Damage(effectDamage.GetDamage());
        
        foreach (Effect effect in effects) // applies all valid effects
        {
            ApplyEffect(effect);
        }
    }

    // function to apply an effect to the boss
    private void ApplyEffect(Effect effect)
    {
        switch (effect.GetEffectType())
        {
            case Effect.EffectType.Damage:
                ApplyEffectDamage(effect);
                break;
            case Effect.EffectType.Slow:
                ApplyEffectSlow(effect);
                break;
            default:
                Debug.LogError("Unimplemented effect: " + effect.GetEffectType());
                return;
        }
    }
    
    // function to apply slow effect to the boss
    private void ApplyEffectSlow(IEffectSlow effectSlow)
    {
        throw new NotImplementedException();
    }
}

