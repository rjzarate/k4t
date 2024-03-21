using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
public class BossReceiveHit : MonoBehaviour, IHittable
{
    public static BossReceiveHit Instance { get; private set; }

    private Health health; // boss's instance of health

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        health = GetComponent<Health>();
    }

    public delegate void BossDamageSoundEventHandler();
    public event BossDamageSoundEventHandler OnBossDamageSoundEvent;

    public void TriggerEffects(List<Effect> effects)
    {
        foreach (Effect effect in effects)
        {
            if (effect.GetEffectType() == Effect.EffectType.Damage)
            {
                ApplyEffectDamage(effect);

                // plays the sound for the boss taking damage
                OnBossDamageSoundEvent();
            }
        }
    }

    private void ApplyEffectDamage(IEffectDamage effectDamage)
    {
        Debug.Log("Damage: " + effectDamage.GetDamage());
        GetComponent<Health>().Damage(effectDamage.GetDamage());
    }
}

