using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayer : MonoBehaviour
{
    // allows for an instance of the GenericPlayer to exist
    private static GenericPlayer instance;
    public static GenericPlayer Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    private static InputHandler inputHandler;


    // variables to set for the GenericPlayer:

    // boolean to determine whether the player is moving or not
    private bool isWalking;

    // movement speed of the player
    [SerializeField] private float moveSpeed = 7f;

    // allows the player to have access to the movement
    [SerializeField] private GenericPlayerMovement genericPlayerMovement;





    // ensures that there is only one player instance
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one Player instance");
        }
        Instance = this;
    }




    // Start is called before the first frame update
    void Start()
    {
        genericPlayerMovement.OnMoveAction += GenericPlayerMovement_OnMoveAction;
        genericPlayerMovement.OnShootAction += GenericPlayerMovement_OnShootAction;
    }





    private void GenericPlayerMovement_OnMoveAction(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void GenericPlayerMovement_OnShootAction(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }





    // Update is called once per frame; handles the player's movement
    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }





    // returns whether the player is currently moving
    public bool IsWalking()
    {
        return isWalking;
    }



    // function to deal with the user's input to affect the player's movement
    private void HandleMovement()
    {
        // inputHandler.OnTapEvent +=;

        int inputComponentInXDirection = genericPlayerMovement.GetMovementComponent();

        Vector3 moveDirection = new Vector3(inputComponentInXDirection, 0f, 0f);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                            playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            // Cannot move towards moveDirection

            // Attempt only X movement
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0);
            canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                                                   playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                moveDirection = moveDirectionX;
            }
            else
            {
                // Cannot move only on the X
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        isWalking = moveDirection != Vector3.zero;

        float rotateSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }


    private void HandleShooting()
    {

    }
}
