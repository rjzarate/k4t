using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    // options for the bounds
    public enum SelectBound { left, right, top, bottom, topLeft, topRight, bottomLeft, bottomRight, middle};

    // Bound Selected, this will be selected in the inspector as a dropdown
    [SerializeField] private SelectBound BoundSelected;

    // The distance affect IF AND ONLY IF the respective axis is Bounded
    [SerializeField] private Vector2 BoundDistance;

    // The screen's width / height
    private float screenRatio;

    // Game is deigned around 9:16, or 0.5625, but for testing purposes I'm doing 16:9
    private float DEFAULT_SCREEN_RATIO = 1.775281f;

    // Initial scale of obj
    private Vector2 initialScale;

    // this this scaler for changing the size
    [SerializeField] private float scaleSize;

    Camera cam;

    void Start()
    {
        // Declared in start to prevent unnecssary looping of this declaration
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        
        AspectRatioManager.Instance.OnCameraChanged += AspectRatioManager_OnCameraChanged;
        
        initialScale = transform.localScale;
        // sets the objects at the right spot and scale at the start
        AspectRatioManager_OnCameraChanged(null, EventArgs.Empty);
    }

    private void AspectRatioManager_OnCameraChanged(object sender, EventArgs e)
    {
        screenRatio = (float)Screen.width / Screen.height;
        scaleObject();

        // newPosition will be updating transform position
        Vector3 newPosition = transform.position;

        // width and hieght of screen in update
        float screenWidth = cam.orthographicSize * cam.aspect;
        float screenHeight = cam.orthographicSize;

        /*
         * 
         * Bound selected will determine:
         * 
         * Which side(s) of the screen the object will be bounded to.
         * There will be BoundDistance that will determine the Offset from 
         * the respective bound.
         * 
         */

        switch (BoundSelected)
        {
            case SelectBound.left:
                newPosition.x = -screenWidth + BoundDistance.x;
                break;
            case SelectBound.right:
                newPosition.x = screenWidth - BoundDistance.x;
                break;
            case SelectBound.top:
                newPosition.y = screenHeight - BoundDistance.y;
                break;
            case SelectBound.bottom:
                newPosition.y = -screenHeight + BoundDistance.y;
                break;
            case SelectBound.topLeft:
                newPosition.x = -screenWidth + BoundDistance.x;
                newPosition.y = screenHeight - BoundDistance.y;
                break;
            case SelectBound.topRight:
                newPosition.x = screenWidth - BoundDistance.x;
                newPosition.y = screenHeight - BoundDistance.y;
                break;
            case SelectBound.bottomLeft:
                newPosition.x = -screenWidth + BoundDistance.x;
                newPosition.y = -screenHeight + BoundDistance.y;
                break;
            case SelectBound.bottomRight:
                newPosition.x = screenWidth - BoundDistance.x;
                newPosition.y = -screenHeight + BoundDistance.y;
                break;
            case SelectBound.middle:
                newPosition.x = BoundDistance.x;
                newPosition.y = BoundDistance.y;
                break;

        }

        // update the transform position
        transform.position = newPosition;
    }


    private void scaleObject()
    {
        float changeSize = screenRatio / DEFAULT_SCREEN_RATIO;
        transform.localScale = initialScale * changeSize * scaleSize;
    }
}