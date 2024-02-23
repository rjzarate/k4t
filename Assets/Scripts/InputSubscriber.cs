using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputHandler.Instance.OnTapEvent += HandleTap;
        InputHandler.Instance.OnPressEvent += HandlePress;
        InputHandler.Instance.OnReleaseEvent += HandleRelease;
        InputHandler.Instance.OnConsecutiveTapEvent += HandleConsecutiveTap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleTap()
    {
        Debug.Log("Tap detected!");
    }

    public void HandleConsecutiveTap()
    {
        Debug.Log("Consecutive Tap detected!");
    }

    public void HandlePress()
    {
        Debug.Log("Press detected!");
    }

    public void HandleRelease()
    {
        Debug.Log("Release detected!");
    }
}
