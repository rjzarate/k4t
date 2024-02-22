using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    GameObject[] arrayScaleObj;
    

    void Start() 
    {
        arrayScaleObj = GameObject.FindGameObjectsWithTag("scaleTag");
    }


    void Update()
    {
        foreach ( GameObject scaleObj in arrayScaleObj) {
            scaleObj.getComponent<AspectRatio>().initialScale;
            scaleObj.getComponent<AspectRatio>();
        }
    }
}
