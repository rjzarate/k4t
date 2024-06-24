using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    // Start is called before the first frame update
    void Start()
    {
        health.DeathEvent += Destroy;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
