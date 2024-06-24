using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D))]
public class ProjectileReceiveHit : MonoBehaviour, IHittable
{
    [SerializeField] private GameObject destroyFX;

    private void Awake()
    {
        // IsTrigger check
        Collider2D collider2D = GetComponent<Collider2D>();
        if (!collider2D.isTrigger)
        {
            Debug.LogWarning("IsTrigger was not set to true. Setting to true");
            collider2D.isTrigger = true;
        }

        // Kinematic check
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D.bodyType != RigidbodyType2D.Kinematic)
        {
            Debug.LogWarning("Body Type was not set to Kinematic. Setting to kinematic.");
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    
    // when this projectile is hit, self destruct
    public void TriggerEffects(List<Effect> effects)
    {
        if (destroyFX)
        {
            Instantiate(destroyFX, transform.position, transform.rotation);
        }
        Destroy(gameObject.transform.parent.gameObject);
    }
}
