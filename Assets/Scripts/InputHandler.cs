using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance
    {
        get; private set;
    }

    private bool heldDown = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Tap!");
            OnTapEvent();
        }
    }

    public delegate void TapEventHandler();
    public event TapEventHandler OnTapEvent;

    public void ConsecutiveTap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Consecutive Tap!");
        }
    }

    public void Press(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (heldDown)
            {
                Debug.Log("Release!");
                OnReleaseEvent();
            }
            heldDown = false;
        }
        else if (context.performed)
        {
            Debug.Log("Press!");
            OnPressEvent();
            heldDown = true;
        }
    }

    public delegate void PressEventHandler();
    public event PressEventHandler OnPressEvent;

    public delegate void ReleaseEventHandler();
    public event ReleaseEventHandler OnReleaseEvent;
}