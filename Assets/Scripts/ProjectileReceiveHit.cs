using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileReceiveHit : MonoBehaviour, IHittable
{
    // when this projectile is hit, self destruct
    public void TriggerEffects<T>(List<T> effects)
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
