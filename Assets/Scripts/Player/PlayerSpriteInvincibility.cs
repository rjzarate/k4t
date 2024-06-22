using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteInvincibility : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Health playerHealth;

    [SerializeField] private Color playerColorDefault = Color.white;
    [SerializeField] private Color playerColorHurt = Color.red;


    private void Start() {
        playerHealth.InvincibilityStartEvent += playerHealth_InvincibilityStartEvent;
        playerHealth.InvincibilityEndEvent += playerHealth_InvincibilityEndEvent;
    }

    private void playerHealth_InvincibilityStartEvent()
    {
        playerSprite.color = playerColorHurt;
    }

        private void playerHealth_InvincibilityEndEvent()
    {
        playerSprite.color = playerColorDefault;
    }
}
