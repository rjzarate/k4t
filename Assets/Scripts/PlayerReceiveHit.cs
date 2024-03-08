using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveHit : MonoBehaviour, IHittable
{
    private Health health; // player's instance of health

    void Start()
    {
        health = GetComponent<Health>();
        Debug.Log("alive!!");
    }

    void Update()
    {
        // i think if the player gets destroyed this script will stop
        Debug.Log("alive");
    }

    // when this projectile is hit, self destruct
    public void TriggerEffects<T>(List<T> effects)
    {
        health.Damage(1); // decrements health if attacked
    }
}
