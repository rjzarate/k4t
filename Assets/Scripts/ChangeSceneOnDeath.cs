using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private string scene;
    // Start is called before the first frame update
    void Start()
    {
        health.DeathEvent += ChangeScene;
    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(scene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}