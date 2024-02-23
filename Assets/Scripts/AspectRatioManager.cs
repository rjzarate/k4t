using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    public static AspectRatioManager Instance { get; private set; }
    [SerializeField] float globalScaleSize = 1;
    public event EventHandler OnCameraChanged;
    private Camera camera;
    private float screenWidth;
    private float screenHeight;

    void Awake() {
        Instance = this;
    }

    private void Start() {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        screenWidth = camera.orthographicSize * camera.aspect;
        screenHeight = camera.orthographicSize;
    }


    void Update()
    {

        float currentScreenWidth = camera.orthographicSize * camera.aspect;
        float currentScreenHeight = camera.orthographicSize;
        if (screenWidth != currentScreenWidth || screenHeight != currentScreenHeight) {
            OnCameraChanged?.Invoke(this, EventArgs.Empty);

            Debug.Log("Camera changed");
            screenWidth = camera.orthographicSize * camera.aspect;
            screenHeight = camera.orthographicSize;
        }
            
    }
}