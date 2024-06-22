using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowUIOnDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        health.DeathEvent += ShowObject;
    }

    public void ShowObject()
    {
        gameObject.SetActive(true);
    }

}