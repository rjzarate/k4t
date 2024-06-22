using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteInvincibility : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Health playerHealth;


    private void Start() {
        playerHealth.InvincibilityStartEvent += playerHealth_InvincibilityStartEvent;
        playerHealth.InvincibilityEndEvent += playerHealth_InvincibilityEndEvent;
    }

    private void playerHealth_InvincibilityStartEvent()
    {
        playerSprite.color = Color.red;
        Debug.Log("Start");
    }

        private void playerHealth_InvincibilityEndEvent()
    {
        playerSprite.color = Color.white;
        Debug.Log("End");
    }
}
