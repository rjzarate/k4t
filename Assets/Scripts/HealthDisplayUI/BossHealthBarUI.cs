using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    private bool fillUpBossHealthBar = true; // used to fill up the boss health to full initially

    [SerializeField] private float fillAmountForInitialLoad = 0.00005f; // used to help fill up the boss health initially

    [SerializeField] private Image barImage;

    private void Start()
    {
        barImage.fillAmount = 0f; // starts off the bar at its lowest

        Show(); // makes sure the bar is being shown
    }

    // TODO: make a script to fill up the bar from of to 1f as the boss spawns

    // TODO: add the health mechanics to the boss and have the bar changes accordingly

    private void Update()
    {
        if (fillUpBossHealthBar) // true if the boss's health hasn't fully loaded
        {
            InitialHealthBarFill();
        }
    }


    // used to fill up the boss's health bar
    private void InitialHealthBarFill()
    {
        if (fillUpBossHealthBar) // true if the bar still needs to fill up
        {
            barImage.fillAmount += fillAmountForInitialLoad;
        }

        if (barImage.fillAmount >= 1f) // true once the boss's health is full
        {
            fillUpBossHealthBar = false;
        }
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

    // reduces the health bar by the amount of damage taken scaled to the bar
    public void Damage(float damage, float maxHealth)
    {
        // float to determine the ratio of damage taken to maxHealth
        float barReduceAmount = damage / maxHealth;

        // reduces bar by said ratio
        barImage.fillAmount -= barReduceAmount;
    }

}
