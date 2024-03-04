using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAndAttackSubscriber : MonoBehaviour
{
    // enums
    public enum PlayerMoveDirection // Labels for movement direction
    {
        Negative_X,
        Positive_X
    }



    // initialize variables:

    private PlayerMoveDirection playerMoveDirection = PlayerMoveDirection.Positive_X; // starting movement direction of right

    private const int AMOUNT_TO_MOVE_IN_NEGATIVE_X = -1; // amount for the player to move in the negative x direction (possibly left)
    private const int AMOUNT_TO_MOVE_IN_POSITIVE_X = 1; // amount for the player to move in the negative x direction (possibly right)

    private bool playerCanMove = false; // variable to determine whether the player can move or not

    [SerializeField] private float moveSpeed = 7f; // configurable speed of the player's movement

    private InputHandler inputHandler; // instance of the inputHandler for the InputSubscriber class



    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to InputHandler tap, press, release, and consecutive tap events
        InputHandler.Instance.OnTapEvent += HandleTap;
        InputHandler.Instance.OnPressEvent += HandlePress;
        InputHandler.Instance.OnReleaseEvent += HandleRelease;
        InputHandler.Instance.OnConsecutiveTapEvent += HandleConsecutiveTap;
    }


    // Update is called once per frame
    void Update()
    {
        // checks whether the player is allowed to move, which is dependent on the press event
        if (playerCanMove) // true if the player is allowed to move
        {
            // calls function to move the player
            PlayerMove();
        }

        // TODO: Implement the HandleTap() and HandleConsecutiveTap() methods
    }



    // What to do on Tap
    public void HandleTap()
    {
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
        playerCanMove = true;
    }


    // What to do when the player releases a press
    public void HandleRelease()
    {
        // used to ensure the event launches correctly
        Debug.Log("Release detected!");

        // switches direction of player movement for next press
        if (playerMoveDirection == PlayerMoveDirection.Negative_X) // true if the current direction for the player to move is left
        {
            // switches the next direction to right
            playerMoveDirection = PlayerMoveDirection.Positive_X;
        }
        else if (playerMoveDirection == PlayerMoveDirection.Positive_X) // true if the current direction for the player to move is right
        {
            // switches the next direction to left
            playerMoveDirection = PlayerMoveDirection.Negative_X;
        }

        // sets the status of playerCanMove to false sicne the player isn't pressing any longer
        playerCanMove = false;
    }




    // function to determine the direction 
    private void PlayerMove()
    {
        // determines the direction that the player will move as the player presses
        if (playerMoveDirection == PlayerMoveDirection.Negative_X) // true if the current direction for the player to move is left
        {
            // moves the player to the left
            PlayerMovement(AMOUNT_TO_MOVE_IN_NEGATIVE_X);

        }
        else if (playerMoveDirection == PlayerMoveDirection.Positive_X) // true if the current direction for the player to move is right
        {
            // moves the player to the right
            PlayerMovement(AMOUNT_TO_MOVE_IN_POSITIVE_X);
        }
    }

    // function to handle the implementation for moving the sprite
    private void PlayerMovement(int playerMovementAmountAlongX)
    {
        // vector for the transformation of the player
        Vector3 playerMovementVector = new Vector3(playerMovementAmountAlongX, 0f, 0f);

        // boolean to check if the player has collied with a wall entity (needs to have a box collider)
        bool playerCanKeepMoving = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 0.9f,
                                                        0.45f, playerMovementVector, moveSpeed * Time.deltaTime);

        // checks to see if the player should'nt move because of a collition
        if (!playerCanKeepMoving) // returns true if there is a collision
        {
            Debug.Log("Player should stop moving!!"); // makes sure the collision is active

            // new position Vecotr3 of <0f, 0f, 0f>
            playerMovementVector = new Vector3(0f, 0f, 0f);
        }

        // transforms the sprite on the x-axis
        transform.position += playerMovementVector * moveSpeed * Time.deltaTime;
    }
}
