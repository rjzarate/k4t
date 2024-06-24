using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private String scene;

    public void LoadCurrentScene()
    {
        SceneManager.LoadSceneAsync(scene);
        Time.timeScale = 1f;
    }
}
