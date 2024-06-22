using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementDirection
    {
        LEFT = -1,
        RIGHT = 1
    }

    private MovementDirection movementDirection = MovementDirection.RIGHT;
    private bool tryMoving = false;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask wallLayerMask;

    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private Health health;

    // Events
    public delegate void WalkSoundEventHandler(bool playerCanMakeWalkingSound);
    public event WalkSoundEventHandler OnWalkSoundEvent;
    
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
    }

    void Update()
    {
        // Runs when the player presses spacebar
        if (tryMoving) Move();
    }
    
    public void HandlePress()
    {
        tryMoving = true;
        OnWalkSoundEvent(true);
    }

    public void HandleRelease()
    {
        tryMoving = false;
        OnWalkSoundEvent(false);
        // Invert direction
        movementDirection = (MovementDirection) ((int) movementDirection * -1);
    }

    // function to handle the implementation for moving the sprite
    private void Move()
    {
        // boolean to check if the player has collied with a wall entity (needs to have a box collider)
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2D.bounds.center, new Vector2((int) movementDirection * 5, 0f), boxCollider2D.bounds.extents.x + 0.09f, wallLayerMask);
        bool isNearWall = (raycastHit.collider != null);
        if (isNearWall)
        {
            Debug.Log("Player should stop moving!!");
            OnWalkSoundEvent(false); // makes the walking noise stop
            return;
        }

        // move player
        transform.position += moveSpeed * Time.deltaTime * new Vector3((int) movementDirection, 0f, 0f);
    }

    private void HandlePause(bool isPaused)
    {
        if (isPaused) {
            ToggleInput(false);
            return;
        }
        ToggleInput(true);
    }

    private void HandleDeath()
    {
        tryMoving = false;
        OnWalkSoundEvent(false);

        // Unsubscribes from player inputs so that the player cannot shoot, move, etc.
        ToggleInput(false);
    }

    private void ToggleInput(bool isEnabled) 
    {
        if (isEnabled)
        {
            Debug.Log("Enabling Player Movement Input");
            // InputHandler.Instance.OnTapEvent += HandleTap;
            InputHandler.Instance.OnPressEvent += HandlePress;
            InputHandler.Instance.OnReleaseEvent += HandleRelease;
            // InputHandler.Instance.OnConsecutiveTapEvent += HandleConsecutiveTap;
            return;
        }

        Debug.Log("Disabling Player Movement Input");
        // InputHandler.Instance.OnTapEvent -= HandleTap;
        InputHandler.Instance.OnPressEvent -= HandlePress;
        InputHandler.Instance.OnReleaseEvent -= HandleRelease;
        // InputHandler.Instance.OnConsecutiveTapEvent -= HandleConsecutiveTap;
    }
}
