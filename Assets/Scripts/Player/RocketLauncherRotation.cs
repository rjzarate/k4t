using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherRotation : MonoBehaviour
{
    private bool isPressed = false;
    [SerializeField] private float rotationMovementMultiplier = 1000f;
    [SerializeField] private float rotationEqualizerMultiplier = 10f;
    [SerializeField] private float rotationReduction = 0.5f;
    [SerializeField] private float rotationVelocityMax = 100f;
    [SerializeField] private float rotationAttackMultiplier = 3f;
    private float rotationVelocity;
    private PlayerMovement playerMovement;

    [SerializeField] private Health health;

    private void Start() 
    {
        playerMovement = Player.Instance.GetPlayerMovement();
        PlayerAttack playerAttack = Player.Instance.GetPlayerAttack();

        ToggleInput(true);

        // Player hitting 0 HP
        health.DeathEvent += HandleDeath;

        playerAttack.AttackEvent += HandleAttack;
    }

    private void HandleAttack()
    {
        // When player attacks, multiply rotation velocity
        rotationVelocity *= rotationAttackMultiplier;
    }


    private void Update()
    {
        if (isPressed && !playerMovement.IsNearWall())
        {
            HandleMovementRotation();
        }

        // If rotated right (360f-180f), use negative (-0f - -180f)
        rotationVelocity -= (transform.eulerAngles.z >= 180f) ? Time.deltaTime * (transform.eulerAngles.z - 360f) * rotationEqualizerMultiplier : Time.deltaTime * transform.eulerAngles.z * rotationEqualizerMultiplier;

        // Max rotation velocity
        if (rotationVelocity > rotationVelocityMax) rotationVelocity = rotationVelocityMax;
        if (rotationVelocity < -rotationVelocityMax) rotationVelocity = -rotationVelocityMax;

        // Update rotation
        transform.Rotate(0f, 0f, Time.deltaTime * rotationVelocity);
        
        // Reduce rotation
        rotationVelocity /= 1f + Time.deltaTime / rotationReduction;
    }

    private void HandleMovementRotation()
    {
        int playerMovementDirection = (int) Player.Instance.GetPlayerMovement().GetMovementDirection();
        rotationVelocity += Time.deltaTime * playerMovementDirection * rotationMovementMultiplier;
    }

    private void ToggleInput(bool isEnabled) 
    {
        if (isEnabled)
        {
            Debug.Log("Enabling Rocket Launcher Rotation Input");
            // InputHandler.Instance.OnTapEvent += HandleTap;
            InputHandler.Instance.OnPressEvent += HandlePress;
            InputHandler.Instance.OnReleaseEvent += HandleRelease;
            // InputHandler.Instance.OnConsecutiveTapEvent += HandleConsecutiveTap;
            return;
        }

        Debug.Log("Disabling Rocket Launcher Rotation Input");
        // InputHandler.Instance.OnTapEvent -= HandleTap;
        InputHandler.Instance.OnPressEvent -= HandlePress;
        InputHandler.Instance.OnReleaseEvent -= HandleRelease;
        // InputHandler.Instance.OnConsecutiveTapEvent -= HandleConsecutiveTap;
    }

    private void HandleRelease()
    {
        isPressed = false;
    }

    private void HandlePress()
    {
        isPressed = true;
    }

    private void HandleDeath()
    {
        isPressed = false;
        ToggleInput(false);
    }
}
