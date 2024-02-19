using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    // options for the bounds
    public enum SelectBound { left, right, top, bottom, topLeft, topRight, bottomLeft, bottomRight };

    // Bound Selected, this will be selected in the inspector as a dropdown
    public SelectBound BoundSelected;

    // The distance affect IF AND ONLY IF the respective axis is Bounded
    public Vector2 BoundDistance;

    Camera cam;

    void Start()
    {
        // Declared in start to prevent unnecssary looping of this declaration
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
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
        }

        // update the transform position
        transform.position = newPosition;
    }
}
