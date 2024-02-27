using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShootRay<T>(Vector2 sourcePosition, Vector2 targetPosition, int layerMask, List<T> hitEffects)
    {
        RaycastHit hit;
        bool success = Physics.Raycast(sourcePosition, targetPosition - sourcePosition, out hit, Mathf.Infinity, layerMask);
        if (!success)
        {
            return;
        }
        Debug.Log("Object Hit");
        IHittable comp = hit.collider.gameObject.GetComponent<IHittable>();
        if (comp != null)
        {
            comp.TriggerEffects(hitEffects);
        }
    }
}