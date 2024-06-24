using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image healthOuter;
    [SerializeField] private Image healthInner;
    [SerializeField] private List<Sprite> outerHealthSprites;
    [SerializeField] private List<Sprite> innerHealthSprites; // should have sprites for health 0 to maxInnerHealth
    [SerializeField] private int maxInnerHealth;

    private Color healthOuterDefaultColor;
    private Color healthInnerDefaultColor;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI(Player.Instance.GetPlayerHealth().GetMaxHealth());
        Player.Instance.GetPlayerHealth().TakeDamageEvent += UpdateUI;
        Player.Instance.GetPlayerHealth().TakeDamageEvent += AnimateUI;
    }

    private void AnimateUI(float newHealth)
    {
        // Animate health to move (wobble)
        healthOuter.GetComponent<Animator>().SetTrigger("TakeDamage");
        healthInner.GetComponent<Animator>().SetTrigger("TakeDamage");
    }

    private void UpdateUI(float health)
    {
        if (health < 0)
        {
            health = 0;
        }
        if (health > maxInnerHealth)
        {
            healthInner.sprite = innerHealthSprites[maxInnerHealth];
            healthOuter.sprite = outerHealthSprites[(int)Mathf.Round(health) - maxInnerHealth];
        }
        else
        {
            healthOuter.sprite = outerHealthSprites[0];
            healthInner.sprite = innerHealthSprites[(int)Mathf.Round(health)];
        }
    }
}
