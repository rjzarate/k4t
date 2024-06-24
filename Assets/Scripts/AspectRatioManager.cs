using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    public static AspectRatioManager Instance { get; private set; }
    // [SerializeField] float globalScaleSize = 1;
    public event EventHandler OnCameraChanged;
    private Camera currentCamera;
    private float screenWidth;
    private float screenHeight;

    void Awake() {
        Instance = this;
    }

    private void Start() {
        currentCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        screenWidth = currentCamera.orthographicSize * currentCamera.aspect;
        screenHeight = currentCamera.orthographicSize;
    }


    void Update()
    {

        float currentScreenWidth = currentCamera.orthographicSize * currentCamera.aspect;
        float currentScreenHeight = currentCamera.orthographicSize;
        if (screenWidth != currentScreenWidth || screenHeight != currentScreenHeight) {
            OnCameraChanged?.Invoke(this, EventArgs.Empty);

            Debug.Log("Camera changed");
            screenWidth = currentCamera.orthographicSize * currentCamera.aspect;
            screenHeight = currentCamera.orthographicSize;
        }
            
    }
}