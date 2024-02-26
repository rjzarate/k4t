using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnTime : MonoBehaviour
{
    //The time in seconds the gameObject has before it gets deleted from the scene, starting from when it was loaded into it
    [SerializeField] private int TotalLifeSeconds;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, TotalLifeSeconds);
    }
}
