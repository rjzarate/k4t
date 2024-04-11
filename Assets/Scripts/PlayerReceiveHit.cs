using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
public class PlayerReceiveHit : MonoBehaviour, IHittable
{
    private Health health; // player's instance of health

    void Start()
    {
        health = GetComponent<Health>();
    }


    public void TriggerEffects(List<Effect> effects)
    {
        foreach(Effect effect in effects) {
            ApplyEffect(effect);
        }
    }

    private void ApplyEffect(Effect effect)
    {
        switch(effect.GetEffectType())
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
        Debug.Log("Damage: " + effectDamage.GetDamage());
        GetComponent<Health>().Damage(effectDamage.GetDamage());
        TakeDamageEvent();
    }

    // event
    public delegate void TakeDamageEventHandler();
    public event TakeDamageEventHandler TakeDamageEvent;
}

