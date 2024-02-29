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

    public void ShootRay<T>(Vector2 sourcePosition, Vector2 targetPosition, int layerMask, List<T> hitEffects)
    {
        Debug.Log("Ray Shot");
        RaycastHit2D hit = Physics2D.Raycast(sourcePosition, targetPosition - sourcePosition, float.PositiveInfinity, layerMask);
        if (!hit)
        {
            return;
        }
        Debug.Log("Object Hit");
        IHittable comp = hit.collider.gameObject.GetComponent<IHittable>();
        if (comp != null)
        {
            comp.TriggerEffects(hitEffects);
        }

        // let the object know it was hit, will call the wasHit() method in the CollisionDetection script of the object
        hit.collider.gameObject.SendMessage("WasHit");

        // trying to call WasHit() directly but ig collisionDetection is null...
        CollisionDetection collisionDetection = hit.collider.gameObject.GetComponent<CollisionDetection>();
        if (collisionDetection != null)
        {
            Debug.Log("Contacted Object");
            collisionDetection.WasHit();
        }


    }
}