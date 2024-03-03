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

        if (chargeSeconds > 0)
        {
            chargeSeconds -= Time.deltaTime;
        }
        
        // charging up done
        if (chargeSeconds <= 0 && state == AttackState.CHARGING)
        {
            state = AttackState.ATTACK;
            GetCurrentPositions();
            // move Boss to ground
            StartCoroutine(MoveOverTime(initialPosition, targetPosition, attackSeconds));
        }
        if (chargeSeconds <= 0 && state == AttackState.RISE)
        {
            state = AttackState.DONE;
            //move Boss to air
            StartCoroutine(MoveOverTime(targetPosition, initialPosition, riseSeconds));
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
            yield return null;
        }
        //transform.position = targetPosition;
        if (state == AttackState.ATTACK)
        {
            state = AttackState.RISE;
        }
    }
}
