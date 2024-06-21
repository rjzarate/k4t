using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAndAttackSubscriber : MonoBehaviour
{
    public static PlayerMovementAndAttackSubscriber Instance { get; private set; }

    // enums
    public enum MovementDirection // Labels for movement direction
    {
        LEFT = -1,
        RIGHT = 1
    }
    // initialize variables:

    private MovementDirection movementDirection = MovementDirection.RIGHT; // starting movement direction of right

    private bool canMove = false; // variable to determine whether the player can move or not

    [SerializeField] private float moveSpeed = 7f; // configurable speed of the player's movement

    private InputHandler inputHandler; // instance of the inputHandler for the InputSubscriber class

    [SerializeField] private LayerMask wallLayerMask;

    private Rigidbody2D rigidBody2D;

    private BoxCollider2D boxCollider2D;
    [SerializeField] private Health health;

    // shooting objects
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float attackDelaySeconds;
    private float attackDelayLeftSeconds;
    [SerializeField] private int maxAmmo;
    private int ammoLeft;
    [SerializeField] private float reloadSeconds;
    private float reloadTimeLeftSeconds;

    private bool dead = false;

    // Events
    public delegate void WalkSoundEventHandler(bool playerCanMakeWalkingSound);
    public event WalkSoundEventHandler OnWalkSoundEvent;
    public delegate void TapSoundEventHandler();
    public event TapSoundEventHandler OnTapSoundEvent;

    public delegate void AmmoCountChangeHandler(int ammoCount);
    public event AmmoCountChangeHandler AmmoCountChangeEvent;
    public delegate void AmmoReloadHandler(float reloadSeconds, float reloadTimeLeftSeconds);
    public event AmmoReloadHandler AmmoReloadEvent;
    public delegate void AmmoReloadToggleHandler(bool showImage);
    public event AmmoReloadToggleHandler AmmoReloadToggleEvent;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rigidBody2D = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        // Subscribe to InputHandler tap, press, release, and consecutive tap events
        ToggleInput(true);
            // InputHandler.Instance.OnTapEvent += HandleTap;
            // InputHandler.Instance.OnPressEvent += HandlePress;
            // InputHandler.Instance.OnReleaseEvent += HandleRelease;
            // InputHandler.Instance.OnConsecutiveTapEvent += HandleConsecutiveTap;
        
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


    // Update is called once per frame
    void Update()
    {
        HandleAttack(); // deals with player's weapon ammo and reload


        // checks whether the player is allowed to move, which is dependent on the press event
        if (canMove) Move();

        // TODO: Implement the HandleTap() and HandleConsecutiveTap() methods
    }

    // What to do on Tap
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

        Debug.Log("Tap detected!");

        // TODO
    }

    


    // What to do on Consecutive Tap
    public void HandleConsecutiveTap()
    {
        
        Debug.Log("Consecutive Tap detected!");

        // TODO
    }


    // What to do when the player starts pressing
    public void HandlePress()
    {
        // used to ensure the event launches correctly
        Debug.Log("Press detected!");

        // sets the status of playerCanMove to true since the player is pressing
        canMove = true;

        OnWalkSoundEvent(canMove); // allows walking noise to play
        
        
    }


    // What to do when the player releases a press
    public void HandleRelease()
    {
        // used to ensure the event launches correctly
        Debug.Log("Release detected!");

        // Invert direction
        movementDirection = (MovementDirection) ((int) movementDirection * -1);

        // sets the status of playerCanMove to false sicne the player isn't pressing any longer
        canMove = false;

        OnWalkSoundEvent(canMove); // allows walking noise to stop
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
        transform.position += new Vector3((int) movementDirection, 0f, 0f) * moveSpeed * Time.deltaTime;
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
        dead = true;
        canMove = false;
        OnWalkSoundEvent(false);

        // Unsubscribes from player inputs so that the player cannot shoot, move, etc.
        ToggleInput(false);
    }

    private void ToggleInput(bool isEnabled) 
    {
        if (isEnabled)
        {
            Debug.Log("Enabling Player Input");
            InputHandler.Instance.OnTapEvent += HandleTap;
            InputHandler.Instance.OnPressEvent += HandlePress;
            InputHandler.Instance.OnReleaseEvent += HandleRelease;
            InputHandler.Instance.OnConsecutiveTapEvent += HandleConsecutiveTap;
            return;
        }

        Debug.Log("Disabling Player Input");
        InputHandler.Instance.OnTapEvent -= HandleTap;
        InputHandler.Instance.OnPressEvent -= HandlePress;
        InputHandler.Instance.OnReleaseEvent -= HandleRelease;
        InputHandler.Instance.OnConsecutiveTapEvent -= HandleConsecutiveTap;
    }
}
