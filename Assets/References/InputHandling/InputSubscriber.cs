using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSubscriber : MonoBehaviour
{
    // enums
    public enum PlayerMoveDirection
    {
        Left,
        Right
    }


    // initialize variables

    private PlayerMoveDirection playerMoveDirection = PlayerMoveDirection.Right; // starting movement direction of right

    private const int AMOUNT_TO_MOVE_LEFT = 1;
    private const int AMOUNT_TO_MOVE_RIGHT = -1;




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
        Debug.Log("Press detected!");

        // checks which direction for the player to move 
        if (playerMoveDirection == PlayerMoveDirection.Left) // true if the current direction for the player to move is left
        {
            // moves the player to the left

            PlayerMovement(AMOUNT_TO_MOVE_LEFT);

        }
        else if (playerMoveDirection == PlayerMoveDirection.Right) // true if the current direction for the player to move is right
        {
            // moves the player to the right

            PlayerMovement(AMOUNT_TO_MOVE_RIGHT);
        }

    }

    // What to do when the player releases a press
    public void HandleRelease()
    {
        Debug.Log("Release detected!");


        // switches direction of player movement for next press
        if (playerMoveDirection == PlayerMoveDirection.Left) // true if the current direction for the player to move is left
        {
            playerMoveDirection = PlayerMoveDirection.Right; // switches the next direction to right
        }
        else if (playerMoveDirection == PlayerMoveDirection.Right) // true if the current direction for the player to move is right
        {
            playerMoveDirection = PlayerMoveDirection.Left; // switches the next direction to left
        }

    }

    private void PlayerMovement(int playerMovementAmountAlongX)
    {
        Vector3 playerMovementVector = new Vector3(playerMovementAmountAlongX, 0f, 0f);

        transform.forward = playerMovementVector;
    }
}
