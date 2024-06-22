using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteAnimator : SpriteAnimator
{
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.GetPlayerMovement().OnWalkSoundEvent += UpdateMove;
        Player.Instance.GetPlayerHealth().TakeDamageEvent += HurtAnimation;
        Player.Instance.GetPlayerHealth().DeathEvent += DeathAnimation;
    }

    private void UpdateMove(bool moving)
    {
        SetBool("Moving", moving);
        if (moving)
        {
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }

    private void HurtAnimation(float health)
    {
        if (health > 0)
        {
            SetTrigger("Hurt");
        }
    }

    private void DeathAnimation()
    {
        SetTrigger("Dead");
    }
}
