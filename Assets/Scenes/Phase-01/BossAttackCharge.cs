using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCharge : BossAction
{
    // TO ADD:
    // - nullify damage while state == AttackState.CHARGING

    private enum AttackState
    {
        CHARGING = 0, ATTACK = 1, RISE = 2, DONE
    }

    [SerializeField] private float chargeSeconds; // charging up time
    private float chargeSecondsDuration;
    [SerializeField] private float attackSeconds; // drop to ground time
    [SerializeField] private float riseSeconds; // go back up time

    [SerializeField] private Transform groundTransform; // the position of the ground
    [SerializeField] private GameObject bossSprite;

    private AttackState state;
    private Vector2 targetPosition;
    private Vector2 initialPosition;
    private float moveDuration;
    private float distanceToTargetPosition;

    // unique action for charge script, is called every frame
    public override void Action()
    {
        duration -= Time.deltaTime;

        // Currently charging
        if (chargeSecondsDuration > 0)
        {
            chargeSecondsDuration -= Time.deltaTime;
            return;
        }
        
        switch (state)
        {
            case AttackState.CHARGING:
                chargeSecondsDuration = chargeSeconds;
                
                state = AttackState.ATTACK;
                GetCurrentPositions();
                // move Boss to ground
                StartCoroutine(MoveOverTime(initialPosition, targetPosition, attackSeconds));
                break;
            case AttackState.ATTACK:
                // Should be currently moving to the ground
                break;
            
            case AttackState.RISE:
                state = AttackState.DONE;
                //move Boss to air
                StartCoroutine(MoveOverTime(targetPosition, initialPosition, riseSeconds));
                break;

            case AttackState.DONE:
                // Should be currently moving up
                break;

            default:
                Debug.LogError("Attack State is not CHARGING, ATTACK, RISE, or DONE: " + state);
                break;
        }
    }

    // get current positions of Boss and the top of the ground
    private void GetCurrentPositions()
    {
        initialPosition = transform.position;

        float bossHeight = bossSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        float topOfGroundHeight = groundTransform.position.y + groundTransform.localScale.y / 2f;
        targetPosition = new Vector2(transform.position.x, topOfGroundHeight + (bossHeight / 2f));

        // distance and speed needed to reach the ground position in attackSeconds
        distanceToTargetPosition = Vector2.Distance(initialPosition, targetPosition);
    }

    // start of this script
    public override void BeginAction()
    {
        duration = chargeSeconds + attackSeconds + riseSeconds;
        chargeSecondsDuration = chargeSeconds;
        state = AttackState.CHARGING;
    }

    // moves from initialPosition to targetPosition over moveDuration
    IEnumerator MoveOverTime(Vector2 initialPosition, Vector2 targetPosition, float moveDuration)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }
        //transform.position = targetPosition;
        if (state == AttackState.ATTACK)
        {
            state = AttackState.RISE;
        }
    }
}
