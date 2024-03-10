using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnvironmentalHitter : MonoBehaviour
{

    [SerializeField] private List<Effect> effects;
    [SerializeField] private float hitDelaySeconds;
    [SerializeField] private float delayRemainingSeconds;
    private HashSet<IHittable> collidingObjects = new HashSet<IHittable>();

    private void Awake()
    {
        // Blacklist check
        Collider2D collider2D = GetComponent<Collider2D>();
        LayerMask excludedLayers = collider2D.excludeLayers;
        if (excludedLayers.value == 0) {
            Debug.LogWarning("No layers are excluded in its collider; Game Object will hit any layer.\n GameObject: " + this);
        }
    }

    private void Start()
    {
        delayRemainingSeconds = hitDelaySeconds;
    }

    private void Update()
    {
        if (delayRemainingSeconds > 0)
        {
            delayRemainingSeconds -= Time.deltaTime;
            if (delayRemainingSeconds <= 0)
            {
                delayRemainingSeconds = hitDelaySeconds;

                foreach (IHittable hittable in collidingObjects.ToListPooled())
                {
                    if (hittable != null)
                    {
                        hittable.TriggerEffects(effects);
                    }
                    else
                    {
                        collidingObjects.Remove(hittable);
                    }
                }
            }
        }
    }

    // when the hitter collides with a valid receiver
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if (hittable == null) return;
        collidingObjects.Add(hittable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IHittable hittable = other.gameObject.GetComponent<IHittable>();
        if (hittable == null) return;
        collidingObjects.Remove(hittable);
    }
}
