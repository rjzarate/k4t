using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    [SerializeField] string[] collidedObjects;
    [SerializeField] GameObject destroyFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (collision.CompareTag(collidedObjects[i]))
            {
                if (destroyFX) { Instantiate(destroyFX, transform.position, transform.rotation); }
                Destroy(gameObject);
            }
        }
    }
}