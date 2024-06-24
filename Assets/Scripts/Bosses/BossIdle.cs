using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossAction
{
    [SerializeField] private float speed; //set to 2 in inspector as of 3/5/2024
    [SerializeField] private int direction = 1;
    [SerializeField] private float leftEdge; //left and right screen boundaries set to -7.5 and 7.5 respectively, 3/5/2024
    [SerializeField] private float rightEdge;
    [SerializeField] private float switchDirectionTimeMinimumSeconds = 3f;
    [SerializeField] private float switchDirectionTimeMaximumSeconds = 5f;
    private float switchDirectionTimeLeft;


    public override void Action()
    {
        MovementIdle();
        duration -= Time.deltaTime;

        switchDirectionTimeLeft -= Time.deltaTime;
        if (switchDirectionTimeLeft <= 0f) {
            direction *= -1;
            switchDirectionTimeLeft = UnityEngine.Random.Range(switchDirectionTimeMinimumSeconds, switchDirectionTimeMaximumSeconds);
        }

    }

    public override void BeginAction()
    {
        duration = time;
        switchDirectionTimeLeft = UnityEngine.Random.Range(switchDirectionTimeMinimumSeconds, switchDirectionTimeMaximumSeconds);
    }

    public void MovementIdle()
    {
        //these ifs are to change direction when boss is near screen boundary
        if (transform.position.x <= leftEdge)
        {
            direction *= -1;
            transform.position = new Vector2(leftEdge, transform.position.y);
            switchDirectionTimeLeft = UnityEngine.Random.Range(switchDirectionTimeMinimumSeconds, switchDirectionTimeMaximumSeconds);
        }
        if (transform.position.x >= rightEdge)
        {
            direction *= -1;
            transform.position = new Vector2(rightEdge, transform.position.y);
            switchDirectionTimeLeft = UnityEngine.Random.Range(switchDirectionTimeMinimumSeconds, switchDirectionTimeMaximumSeconds);
        }

        //sets position based on speed and direction, only moves left and right
        transform.position = new Vector2(transform.position.x + (speed * direction) * Time.deltaTime, transform.position.y);
    }
}
