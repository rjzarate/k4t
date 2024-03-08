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

        if (!isAlive) // true if the player dies
        {
            Destroy(gameObject.transform.parent.gameObject); // deletes the player
            Debug.Log("death");
        }
        else
        {
            Debug.Log("living");
        }


    // when this projectile is hit, self destruct
    public void TriggerEffects<T>(List<T> effects)
    {
        health.Damage(1); // decrements health if attacked
    }
}
