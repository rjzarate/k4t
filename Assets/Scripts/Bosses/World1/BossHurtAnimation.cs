using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHurtAnimation : MonoBehaviour
{
    [SerializeField] private Health bossHealth;
    [SerializeField] private SpriteRenderer bossSprite;
    [SerializeField] private float showHurtDuration = 0.2f;

    [SerializeField] private Color bossColorDefault = Color.white;
    [SerializeField] private Color bossColorMinimumHurt = Color.red;
    [SerializeField] private Color bossColorMaximumHurt = Color.red;


    private void Start() {
        bossHealth.TakeDamageEvent += HandleTakeDamage;
    }

    private void HandleTakeDamage(float newHealth)
    {
        // Find difference between max and min multiplied by health difference
        float r = (bossColorMaximumHurt.r - bossColorMinimumHurt.r) * (bossHealth.GetMaxHealth() - newHealth) / bossHealth.GetMaxHealth();
        float g = (bossColorMaximumHurt.g - bossColorMinimumHurt.g) * (bossHealth.GetMaxHealth() - newHealth) / bossHealth.GetMaxHealth();
        float b = (bossColorMaximumHurt.b - bossColorMinimumHurt.b) * (bossHealth.GetMaxHealth() - newHealth) / bossHealth.GetMaxHealth();
        float a = (bossColorMaximumHurt.a - bossColorMinimumHurt.a) * (bossHealth.GetMaxHealth() - newHealth) / bossHealth.GetMaxHealth();

        bossSprite.color = new Color(r + bossColorMinimumHurt.r, g + bossColorMinimumHurt.g, b + bossColorMinimumHurt.b, a + bossColorMinimumHurt.a);

        StopAllCoroutines();
        StartCoroutine(ShowHurtRoutine(showHurtDuration));
    }

    private IEnumerator ShowHurtRoutine(float showHurtDuration)
    {
        yield return new WaitForSeconds(showHurtDuration);
        bossSprite.color = bossColorDefault;
    }
}
