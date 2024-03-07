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
        bool isAlive = health.death(); // determines whether the player is still alive

        if (!isAlive) // true if the player dies
        {
            Destroy(gameObject.transform.parent.gameObject); // deletes the player
        }
    }

    // when this projectile is hit, self destruct
    public void TriggerEffects<T>(List<T> effects)
    {
        health.lowerHealth(); // decrements health if attacked
    }
}
