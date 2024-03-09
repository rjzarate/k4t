using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;

    private void Start()
    {
        barImage.fillAmount = 1f; // starts off the bar at its lowest



        Show(); // makes sure the bar is being shown
    }


    // TODO: make a script to fill up the bar from of to 1f as the boss spawns

    // TODO: add the health mechanics to the boss and have the bar changes accordingly


    // call to show the bar
    private void Show()
    {
        gameObject.SetActive(true);
    }

    // call to hide the bar
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
