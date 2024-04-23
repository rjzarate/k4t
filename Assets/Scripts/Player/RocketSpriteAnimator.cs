using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpriteAnimator : SpriteAnimator
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementAndAttackSubscriber.Instance.OnTapSoundEvent += ShootAnimation;
    }

    private void ShootAnimation()
    {
        SetTrigger("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
