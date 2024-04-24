using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // attach this script to UI pause button
    [SerializeField] GameObject pauseScreen;
    private bool isPaused = false;

    public void TogglePause()
    {
        Debug.Log("pause");
        if (isPaused)
        {
            Time.timeScale = 1.0f;
            isPaused = false;
            //HidePauseScreen();
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
            //ShowPauseScreen();
        }
    }

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }
    
}
