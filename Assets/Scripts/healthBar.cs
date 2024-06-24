using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject healthObj;

    void Start()
    {
        
    }

    void Update()
    {
        if (healthObj.GetComponent<Health>())
        {
            slider.maxValue = healthObj.GetComponent<Health>().GetMaxHealth();
            slider.value = healthObj.GetComponent<Health>().GetHealth() / healthObj.GetComponent<Health>().GetMaxHealth();
        }
    }
}