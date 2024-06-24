using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class ConstantRotation : MonoBehaviour
{
    [Range(-1800, 1800)]
    [SerializeField] private float rotation = 10;
    [SerializeField] private bool randomInvertedRotation = true;

    private Vector3 rotationVector;

    private void Start() {
        // If to invert rotation
        int invertRotation = 1;
        if (randomInvertedRotation) {
            System.Random random = new System.Random();
            invertRotation = (random.Next(0, 1) == 0) ? -1 : 1;
        }

        rotationVector = new Vector3(0, 0, rotation * invertRotation);

    }
    
    private void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime);
    }
}
