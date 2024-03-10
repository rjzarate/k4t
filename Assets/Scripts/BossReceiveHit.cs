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
        foreach (Effect effect in effects)
        {
            ApplyEffect(effect);
        }
    }

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

    private void ApplyEffectSlow(IEffectSlow effectSlow)
    {
        throw new NotImplementedException();
    }

    private void ApplyEffectDamage(IEffectDamage effectDamage)
    {
        bossHealthBarUI.Damage(effectDamage.GetDamage(), health.GetMaxHealth());
        health.Damage(effectDamage.GetDamage());

        Debug.Log("Damage: " + effectDamage.GetDamage());
    }
}

