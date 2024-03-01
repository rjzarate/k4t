using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTargetTest : MonoBehaviour, IHittable
{
    // test method for player to receive effects
    public void TriggerEffects<T>(List<T> effects)
    {
        Debug.Log(effects);
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
