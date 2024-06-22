using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteAnimator : SpriteAnimator
{
    [SerializeField] private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement.OnWalkSoundEvent += UpdateMove;
        PlayerReceiveHit.PlayerHealth.TakeDamageEvent += HurtAnimation;
        PlayerReceiveHit.PlayerHealth.DeathEvent += DeathAnimation;
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
