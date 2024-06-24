using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherFireParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

    private void Start() {
        Player.Instance.GetPlayerAttack().AttackEvent += FireParticles;
    }

    private void FireParticles() {
        particleSystem.Play();
    }
}
