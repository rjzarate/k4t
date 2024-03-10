using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyForce : MonoBehaviour
{
    // this is just applying initial force forward

    public float initForce;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb)
        {
            Vector2 force = transform.up * initForce;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}