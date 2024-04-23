using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private PlayerMovementAndAttackSubscriber playerAttack;
    [SerializeField] private Image ammoImage;
    [SerializeField] private Image ammoReloadBar;
    [SerializeField] private List<Sprite> ammoSprites;
    [SerializeField] private int startingAmmo;
    // Start is called before the first frame update
    void Start()
    {
        UpdateAmmoUI(startingAmmo);
        playerAttack.AmmoCountChangeEvent += UpdateAmmoUI;
        playerAttack.AmmoReloadEvent += UpdateAmmoBarUI;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateAmmoUI(int ammo)
    {
        if (ammo < 0)
        {
            ammo = 0;
        }
        ammoImage.sprite = ammoSprites[ammo];
    }

    private void UpdateAmmoBarUI(float reloadSeconds, float reloadTimeLeftSeconds) {
        Debug.Log("test");
    }
}
