using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnCollision : MonoBehaviour
{
    // include existing tags for collision detection
    [SerializeField] string[] collisionTags;

    // if any of the existing tags are collided along with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach ( string collisiontag in collisionTags)
        {
            if (collision.collider.CompareTag(collisiontag))
            {
                Destroy(gameObject);
            }
        }
    }
}