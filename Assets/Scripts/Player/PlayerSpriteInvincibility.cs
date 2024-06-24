using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteInvincibility : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    private Health playerHealth;

    private Color playerColorDefault;
    [SerializeField] private Color playerColorHurt = Color.red;


    private void Start() {
        playerColorDefault = playerSprite.color;

        playerHealth = Player.Instance.GetPlayerHealth();
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
