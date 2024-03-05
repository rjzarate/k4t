using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IHittable
{
    public void TriggerEffects<T>(List<T> effects)
    {
        Debug.Log("Testing");
        throw new System.NotImplementedException();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
