using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
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
        }
    }

    public void ConsecutiveTap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Consecutive Tap!");
        }
    }

    public void Press(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Press!");
        }
        else if (context.canceled)
        {
            Debug.Log("Release!");
        }
    }
}