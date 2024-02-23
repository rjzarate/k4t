using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    AspectRatio[] arrayScaleObj;
    [SerializeField] float globalScaleSize = 1;


    void Start() 
    {
        arrayScaleObj = GameObject.FindObjectsOfType<AspectRatio>();
    }


    void Update()
    {
        // Loop through the array of GameObjects and do something with them
        foreach (AspectRatio aspectRatioObject in arrayScaleObj)
        {
            aspectRatioObject.scaleSize = globalScaleSize;
        }
    }
}