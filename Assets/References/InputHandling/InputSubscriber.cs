using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSubscriber : MonoBehaviour
{
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
    }

    // What to do on Consecutive Tap
    public void HandleConsecutiveTap()
    {
        Debug.Log("Consecutive Tap detected!");
    }

    // What to do when the player starts pressing
    public void HandlePress()
    {
        Debug.Log("Press detected!");
    }

    // What to do when the player releases a press
    public void HandleRelease()
    {
        Debug.Log("Release detected!");
    }
}
