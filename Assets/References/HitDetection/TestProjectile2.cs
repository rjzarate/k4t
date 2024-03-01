using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Time.deltaTime, 0, 0);
    }
}
