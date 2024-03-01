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
        // used to ensure the event HandleTap launches correctly
        Debug.Log("Tap detected!");

        // TODO
    }


    // What to do on Consecutive Tap
    public void HandleConsecutiveTap()
    {
        // used to ensure the event HandleConsecutiveTap launches correctly
        Debug.Log("Consecutive Tap detected!");

        // TODO
    }


    // What to do when the player starts pressing
    public void HandlePress()
    {
        // used to ensure the event HandlePress launches correctly
        Debug.Log("Press detected!");

        // TODO
    }


    // What to do when the player releases a press
    public void HandleRelease()
    {
        // used to ensure the event HandleRelease launches correctly
        Debug.Log("Release detected!");

        // TODO
    }
}
