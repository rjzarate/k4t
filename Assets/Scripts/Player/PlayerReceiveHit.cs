using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveHit : MonoBehaviour, IHittable
{
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
            case Effect.EffectType.Invincibility:
                ApplyEffectInvincibility(effect);
                break;
            case Effect.EffectType.Slow:
                ApplyEffectSlow(effect);
                break;
            default:
                Debug.LogError("Unimplemented effect: " + effect.GetEffectType());
                return;
        }
    }

    private void ApplyEffectInvincibility(IEffectInvincibility effectInvincibility)
    {
        float invincibility = effectInvincibility.GetInvincibility();
        Debug.Log("Invincibility Effect: " + invincibility);
        Player.Instance.GetPlayerHealth().SetInvincibility(invincibility);

    }

    private void ApplyEffectSlow(IEffectSlow effectSlow)
    {
        throw new NotImplementedException();
    }

    private void ApplyEffectDamage(IEffectDamage effectDamage)
    {
        float damage = effectDamage.GetDamage();
        Debug.Log("Damage Effect: " + damage);
        Player.Instance.GetPlayerHealth().Damage(damage);
    }
}

