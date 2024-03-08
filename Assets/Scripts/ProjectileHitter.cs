using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitter : MonoBehaviour
{
    [SerializeField] private List<string> targetLayers;
    // Change later
    [SerializeField] private List<object> effects;

    private void Start()
    {
        targetLayers.Add("Player");
    }

    // when the hitter collides with a valid receiver
    // self destructs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayers.Contains(LayerMask.LayerToName(collision.gameObject.layer)))
        {
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.TriggerEffects(effects);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}
