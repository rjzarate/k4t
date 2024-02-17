using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveTest : MonoBehaviour
{
    [Header("Character Movements:")]
    public float speed;
    public bool facingRight = true;
    public Rigidbody2D rb;
    Vector2 dir;

    [Header("Bounds:")]
    public float scaleBounds;
    float camHalfWidth;

    void Start()
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camHalfWidth = cam.orthographicSize * cam.aspect;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            flip();
        }

        if (facingRight)
        {
            dir.x = 1f;
        }

        else {
            dir.x = -1f;
        }

        flipBound();

        rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        transform.localScale = flipScale;
    }

    void flipBound()
    {
        if (transform.position.x > camHalfWidth - scaleBounds && facingRight)
        {
            flip();
        }

        else if (transform.position.x < -camHalfWidth + scaleBounds && !facingRight)
        {
            flip();
        }
    }
}