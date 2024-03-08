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
            default:
                Debug.LogError("Unimplemented effect: " + effect.GetEffectType());
                return;
        }
    }

    private void ApplyEffectDamage(IEffectDamage effectDamage)
    {
        Debug.Log("Damage: " + effectDamage.GetDamage());
    }
}

