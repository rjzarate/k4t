using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;

    private void Start()
    {
        barImage.fillAmount = 0f; // starts off the bar at its lowest

        Show(); // makes sure the bar is being shown
    }



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
