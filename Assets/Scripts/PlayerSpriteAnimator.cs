using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteAnimator : SpriteAnimator
{
    [SerializeField] private PlayerReceiveHit hitReceiver;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementAndAttackSubscriber.Instance.OnWalkSoundEvent += UpdateMove;
        hitReceiver.TakeDamageEvent += HurtAnimation;
    }

    private void UpdateMove(bool moving)
    {
        SetBool("Moving", moving);
        if (moving)
        {
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }

    private void HurtAnimation()
    {
        SetTrigger("Hurt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
