using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private Health health;

    // shooting objects
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float attackDelaySeconds = 0.4f;
    private float attackDelayLeftSeconds;
    [SerializeField] private int maxAmmo = 4;
    private int ammoLeft;
    [SerializeField] private float reloadSeconds = 3;
    private float reloadTimeLeftSeconds;

    // Events
    public delegate void TapSoundEventHandler();
    public event TapSoundEventHandler OnTapSoundEvent;
    public delegate void AmmoCountChangeHandler(int ammoCount);
    public event AmmoCountChangeHandler AmmoCountChangeEvent;
    public delegate void AmmoReloadHandler(float reloadSeconds, float reloadTimeLeftSeconds);
    public event AmmoReloadHandler AmmoReloadEvent;
    public delegate void AmmoReloadToggleHandler(bool showImage);
    public event AmmoReloadToggleHandler AmmoReloadToggleEvent;

    void Start()
    {
        rigidBody2D = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        // Subscribe to InputHandler tap, press, release, and consecutive tap events
        ToggleInput(true);
        
        // Pausing
        Pause.Instance.PauseToggleEvent += HandlePause;
        
        // Player hitting 0 HP
        health.DeathEvent += HandleDeath;
        
        // Fill ammo to full
        ammoLeft = maxAmmo;
    }

    private void HandlePause(bool isPaused)
    {
        if (isPaused) {
            ToggleInput(false);
            return;
        }
        ToggleInput(true);
    }

    void Update()
    {
        HandleAttack();
    }

    public void HandleTap()
    {
        if (reloadTimeLeftSeconds <= 0 && attackDelayLeftSeconds <= 0)
        {
            Instantiate(bulletObject, firingPoint.position, firingPoint.rotation);
            OnTapSoundEvent();
            attackDelayLeftSeconds = attackDelaySeconds;
            ammoLeft--;
            AmmoCountChangeEvent(ammoLeft);
            if (ammoLeft <= 0)
            {
                reloadTimeLeftSeconds = reloadSeconds;
                AmmoReloadToggleEvent(true);
            }
        }
    }

    // function to handle the reloading and ammo count of the player's weapon
    private void HandleAttack()
    {
        if (reloadTimeLeftSeconds > 0)
        {
            reloadTimeLeftSeconds -= Time.deltaTime;

            // Call event that weapon is reloading
            AmmoReloadEvent(reloadSeconds, reloadTimeLeftSeconds);

            if (reloadTimeLeftSeconds < 0)
            {
                ammoLeft = maxAmmo;
                AmmoCountChangeEvent(ammoLeft);
                AmmoReloadToggleEvent(false);
                reloadTimeLeftSeconds = 0;
            }
        }

        if (attackDelayLeftSeconds > 0)
        {
            attackDelayLeftSeconds -= Time.deltaTime;
        }
    }

    private void HandleDeath()
    {
        // Unsubscribes from player inputs so that the player cannot shoot, move, etc.
        ToggleInput(false);
    }

    private void ToggleInput(bool isEnabled) 
    {
        if (isEnabled)
        {
            Debug.Log("Enabling Player Attack Input");
            InputHandler.Instance.OnTapEvent += HandleTap;
            // InputHandler.Instance.OnPressEvent += HandlePress;
            // InputHandler.Instance.OnReleaseEvent += HandleRelease;
            // InputHandler.Instance.OnConsecutiveTapEvent += HandleConsecutiveTap;
            return;
        }

        Debug.Log("Disabling Player Attack Input");
        InputHandler.Instance.OnTapEvent -= HandleTap;
        // InputHandler.Instance.OnPressEvent -= HandlePress;
        // InputHandler.Instance.OnReleaseEvent -= HandleRelease;
        // InputHandler.Instance.OnConsecutiveTapEvent -= HandleConsecutiveTap;
    }
}
