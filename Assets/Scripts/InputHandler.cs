using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Singleton instance
    public static InputHandler Instance
    {
        get; private set;
    }

    private bool heldDown = false;
    private float timeLeftToTapInSeconds = 0;
    // For taps to be considered consecutive, must be done with less than the below time in between
    [SerializeField] private float consecutiveTapWindowSeconds = 0.5f;

    private void Awake()
    {
        // slashed out for now since the player inputs won't work when the game starts
        // feel free to edit it later

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
    // If time left to tap is above 0, decrements using delta time
    void Update()
    {
        if (timeLeftToTapInSeconds > 0)
        {
            timeLeftToTapInSeconds -= Time.deltaTime;
            if (timeLeftToTapInSeconds < 0)
            {
                timeLeftToTapInSeconds = 0;
            }
        }
    }

    // Triggers tap event, triggers consecutive tap if valid
    public void Tap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Tap!");
            OnTapEvent?.Invoke();
            if(timeLeftToTapInSeconds > 0)
            {
                Debug.Log("Consecutive Tap!");
                OnConsecutiveTapEvent?.Invoke();
            }
            timeLeftToTapInSeconds = consecutiveTapWindowSeconds;
        }
    }

    public delegate void TapEventHandler();
    public event TapEventHandler OnTapEvent;

    public delegate void ConsecutiveTapEventHandler();
    public event ConsecutiveTapEventHandler OnConsecutiveTapEvent;

    // Triggers press event and release event when done
    public void Press(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (heldDown)
            {
                Debug.Log("Release!");
                OnReleaseEvent?.Invoke();
            }
            heldDown = false;
        }
        else if (context.performed)
        {
            Debug.Log("Press!");
            OnPressEvent?.Invoke();
            heldDown = true;
        }
    }

    public delegate void PressEventHandler();
    public event PressEventHandler OnPressEvent;

    public delegate void ReleaseEventHandler();
    public event ReleaseEventHandler OnReleaseEvent;
}