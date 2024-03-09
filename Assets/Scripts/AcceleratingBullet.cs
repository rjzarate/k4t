using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratingBullet : Bullet
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private Vector2 acceleration;

    // initialize bullet movement stats
    public void Initialize(Vector2 velocity, Vector2 acceleration)
    {
        this.velocity = velocity;
        this.acceleration = acceleration;
    }

    // bullet traveling upwards with respect to its own rotation
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        velocity += acceleration * Time.deltaTime;
    }
}