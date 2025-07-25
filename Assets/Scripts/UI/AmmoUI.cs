using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Image ammoImage;
    [SerializeField] private Image ammoReloadBar;
    [SerializeField] private List<Sprite> ammoSprites;
    [SerializeField] private int startingAmmo;

    private Color ammoColorDefault;
    [SerializeField] private Color ammoColorNoAmmo = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        ammoColorDefault = ammoImage.color;
        
        UpdateAmmoUI(startingAmmo);
        PlayerAttack playerAttack = Player.Instance.GetPlayerAttack();
        playerAttack.AmmoCountChangeEvent += UpdateAmmoUI;
        playerAttack.AmmoReloadEvent += UpdateAmmoBarUI;
        playerAttack.AmmoReloadToggleEvent += ShowAmmoBar;

        // Hide reload bar
        ammoReloadBar.gameObject.SetActive(false);
    }

    private void UpdateAmmoUI(int ammo)
    {
        if (ammo < 0)
        {
            ammo = 0;
        }
        ammoImage.sprite = ammoSprites[ammo];

        // Visuals for when ammo is 0
        if (ammo == 0) {
            ammoImage.color = ammoColorNoAmmo;
        } else {
            ammoImage.color = ammoColorDefault;
        }
    }

    private void UpdateAmmoBarUI(float reloadSeconds, float reloadTimeLeftSeconds) {
        ammoReloadBar.fillAmount = 1 - (reloadTimeLeftSeconds / reloadSeconds);
    }

    private void ShowAmmoBar(bool showImage)
    {
        ammoReloadBar.gameObject.SetActive(showImage);
    }
}
