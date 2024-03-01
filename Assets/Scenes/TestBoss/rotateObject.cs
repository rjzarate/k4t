using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    // speed of the rotation
    // negative is counterclockwise
    // positive is clockwise
    [SerializeField]float rotateSpeed;


    // rotate along the z axis, since it's 2D
    void Update()
    {
        transform.Rotate(0,0, rotateSpeed *Time.deltaTime);
    }
}