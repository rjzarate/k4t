using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileHitter : MonoBehaviour
{

    [SerializeField] private List<Effect> effects;
    [SerializeField] private bool destroyOnHit = true;
    [SerializeField] private GameObject destroyFX;

    [SerializeField] private ParticleSystem[] particlesKept;

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
        if (hittable != null)
        {
            hittable.TriggerEffects(effects);
        }
        
        // Self destruct
        if (destroyOnHit)
        {
            // Keep particles on self distruct. Detaches particle system and them destroys for it's given particle duration
            foreach (ParticleSystem particleSystem in particlesKept) {
                particleSystem.transform.parent = null;
                particleSystem.Stop();
                Destroy(particleSystem, particleSystem.main.duration + 1f);
            }
            
            // Effects on self distruct
            if (destroyFX)
            {
                Instantiate(destroyFX, transform.position, transform.rotation);
            }
            
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
