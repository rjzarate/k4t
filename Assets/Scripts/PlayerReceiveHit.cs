using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveHit : MonoBehaviour, IHittable
{
    private Health health; // player's instance of health

    void start()
    {
        health = GetComponent<Health>();
    }

    void update()
    {

    }

    // when this projectile is hit, self destruct
    public void TriggerEffects<T>(List<T> effects)
    {
        health.Damage(1); // decrements health if attacked
    }
}
