using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // bullet travel speed
    public float speed;

    // bullet traveling upwards with respect to its own rotation
    void Update()
    {
        transform.Translate(Vector2.up*speed*Time.deltaTime);
    }
}