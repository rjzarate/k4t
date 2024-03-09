using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileHitter : MonoBehaviour
{

    [SerializeField] private List<Effect> effects;
    [SerializeField] private bool destroyOnHit = true;

    private void Awake()
    {
        // Blacklist check
        Collider2D collider2D = GetComponent<Collider2D>();
        LayerMask excludedLayers = collider2D.excludeLayers;
        if (excludedLayers.value == 0) {
            Debug.LogWarning("No layers are excluded in its collider; Game Object will hit any layer.\n GameObject: " + this);
        }
    }

    // when the hitter collides with a valid receiver
    // self destructs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if (hittable == null) return;
        
        hittable.TriggerEffects(effects);
        if (destroyOnHit) Destroy(gameObject.transform.parent.gameObject);
    }
}
