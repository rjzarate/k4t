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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
