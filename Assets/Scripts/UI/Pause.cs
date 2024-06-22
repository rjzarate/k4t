using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Pause : MonoBehaviour
{
    public static Pause Instance { get; private set; }

    // attach this script to UI pause button
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Image pauseImage;
    private bool isPaused = false;


    private void Awake()
    {
        Instance = this;
    }

    public void TogglePause()
    {
        Debug.Log("pause");

        // Deselects button so SPACEBAR doesn't unpause
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem .GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

        // Invert pause
        PauseToggleEvent(!isPaused);
        isPaused = !isPaused;

        // When paused, set time stops
        if (isPaused)
        {
            Time.timeScale = 0f;
            TogglePauseScreen(true);
            pauseImage.color = Color.white;
            return;
        }

        // When unpaused, time resumes
        Time.timeScale = 1.0f;
        TogglePauseScreen(false);
        pauseImage.color = Color.black;
    }

    public void TogglePauseScreen(bool toggle)
    {
        pauseScreen.SetActive(toggle);
    }
    
    public delegate void PauseToggleHandler(bool isPaused);
    public event PauseToggleHandler PauseToggleEvent;
}
