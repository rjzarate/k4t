using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossAction
{
    [SerializeField] private float speed; //set to 2 in inspector as of 3/5/2024
    [SerializeField] private int direction = 1;
    [SerializeField] private float leftEdge; //left and right screen boundaries set to -7.5 and 7.5 respectively, 3/5/2024
    [SerializeField] private float rightEdge;

    public override void Action()
    {
        MovementIdle();
        duration -= Time.deltaTime;
    }

    public override void BeginAction()
    {
        duration = time;
    }

    public void MovementIdle()
    {
        //these ifs are to change direction when boss is near screen boundary
        if (transform.position.x <= leftEdge)
        {
            direction *= -1;
            transform.position = new Vector2(leftEdge, transform.position.y);
        }
        if (transform.position.x >= rightEdge)
        {
            direction *= -1;
            transform.position = new Vector2(rightEdge, transform.position.y);
        }

        //sets position based on speed and direction, only moves left and right
        transform.position = new Vector2(transform.position.x + (speed * direction) * Time.deltaTime, transform.position.y);
    }
}
