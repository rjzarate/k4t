using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReceiveHit : MonoBehaviour, IHittable
{
    private Health health; // boss's instance of health

    void Start()
    {
        health = Boss.Instance.GetBossHealth();
    }

    public void TriggerEffects(List<Effect> effects)
    {
        foreach (Effect effect in effects)
        {
            if (effect.GetEffectType() == Effect.EffectType.Damage)
            {
                ApplyEffectDamage(effect);
            }
        }
    }

    private void ApplyEffectDamage(IEffectDamage effectDamage)
    {
        Debug.Log("Damage: " + effectDamage.GetDamage());
        health.Damage(effectDamage.GetDamage());
    }
}

