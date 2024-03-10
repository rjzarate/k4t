using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    // handles the summoning and rotation of the ricochets

    [SerializeField] GameObject ricochetBullets;
    [SerializeField] float[] bulletRotationSpread;
    [SerializeField] float[] initForces;

    void Start()
    {
        for (int i = 0; i < bulletRotationSpread.Length; i++)
        {
            GameObject bulletSub = Instantiate(ricochetBullets, transform.position, Quaternion.Euler(0, 0, bulletRotationSpread[i]));

            if (bulletSub.GetComponent<applyForce>() && initForces.Length >= i)
            {
                bulletSub.GetComponent<applyForce>().initForce = initForces[i];
            }
        }
    }
}