using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[Serializable]
public sealed class Effect : IEffectDamage, IEffectInvincibility, IEffectSlow, IEffectPoison
{
    public enum EffectType {
        Null, Damage, Invincibility, Slow, Poison
    }

    [SerializeField] private EffectType effectType = EffectType.Null;
    
    [SerializeField] private float genericFloat = 0;
    [SerializeField] private float genericFloat1 = 0;
    [SerializeField] private float genericFloat2 = 0;

    [SerializeField] [Range(0, 1)] private float genericUnitInterval = 0;

    float IEffectDamage.GetDamage() {
        if (effectType != EffectType.Damage) {
            Debug.LogError("Effect is not a Damage Effect: " + effectType);
        }

        return genericFloat;
    }

    float IEffectInvincibility.GetInvincibility()
    {
        return genericFloat;
    }

    float IEffectSlow.GetSlow() {
        if (effectType != EffectType.Slow) {
            Debug.LogError("Effect is not a Slow Effect: " + effectType);
        }

        return genericUnitInterval;
    }

    public EffectType GetEffectType()
    {
        return effectType;
    }

    float[] IEffectPoison.GetPoison()
    {
        return new float[1];
    }

    
}