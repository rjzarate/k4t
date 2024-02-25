using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayerMovement : MonoBehaviour
{

    /**
    // event for when the user taps on the screen 
    public event EventHandler OnMoveAction;
    public event EventHandler OnShootAction;


    public delegate void TapEventHandler();
    public event TapEventHandler OnTapEvent;

    public delegate void ConsecutiveTapEventHandler();
    public event ConsecutiveTapEventHandler OnConsecutiveTapEvent;

    public delegate void PressEventHandler();
    public event PressEventHandler OnPressEvent;

    public delegate void ReleaseEventHandler();
    public event ReleaseEventHandler OnReleaseEvent;


    // Instance of the GenericPlayerMovement
    public static GenericPlayerMovement Instance { get; private set; }

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    private string directionOfMovement = "";

    //
    public enum Binding
    {
        Move_Left,
        Move_Right,
        Shoot
    }

    
    private PlayerInputActions playerInputActions;


    // Awake starts as soon as the game launches
    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

       


        /*
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
        */

/*

        // Allows the user to make player actions
        playerInputActions.Player.Enable();

        // adds the events for player move and shoot
        playerInputActions.Player.Move.performed += Move_performed;
        playerInputActions.Player.Shoot.performed += Shoot_performed;

    }

    // As the game closes, it destroys all the playerInputActions events
    private void OnDestroy()
    {
        playerInputActions.Player.Move.performed -= Move_performed;
        playerInputActions.Player.Shoot.performed -= Shoot_performed;

        playerInputActions.Dispose();
    }



    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log(context);
        OnMoveAction?.Invoke(this, EventArgs.Empty);
    }

    private void Shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnShootAction?.Invoke(this, EventArgs.Empty);
    }

    // function to return the vector to represent the player's movement
    public int GetMovementComponent()
    {

        int inputVector = playerInputActions.Player.Move.ReadValue<int>();

        return inputVector;
    }

    

    **/
}

