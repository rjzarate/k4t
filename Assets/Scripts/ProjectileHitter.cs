using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitter : MonoBehaviour
{
    [SerializeField] private List<string> targets;
    // Change later
    [SerializeField] private List<string> effects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    // when the hitter collides with a valid receiver
    // self destructs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targets.Contains(LayerMask.LayerToName(collision.gameObject.layer)))
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
