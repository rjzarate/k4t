using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private PlayerMovementAndAttackSubscriber playerAttack;
    [SerializeField] private Image ammoImage;
    [SerializeField] private List<Sprite> ammoSprites;
    [SerializeField] private int startingAmmo;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI(startingAmmo);
        playerAttack.AmmoCountChangeEvent += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateUI(int ammo)
    {
        if (ammo < 0)
        {
            ammo = 0;
        }
        ammoImage.sprite = ammoSprites[ammo];
    }
}
