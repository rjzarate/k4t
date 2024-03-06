using System;
using UnityEngine;

public class BossAttackStageHazard : BossAction
{
    [SerializeField] private float chargeSeconds;
    [SerializeField] private float attackSeconds; // time for the meateor to hit the ground after being made
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject stageHazard;
    [SerializeField] private Transform groundTransform; // the position of the ground

    private Vector2 targetPosition;
    private float bulletHeight;
    private float chargeDuration;
    private float attackDuration;

    private enum AttackState
    {
        CHARGING = 0, ATTACK = 1, STAGE_HAZARD = 2, DONE
    }

    private AttackState state;

    // BeginAction is called at the start of this script
    public override void BeginAction()
    {
        Debug.Log("begin");
        duration = chargeSeconds + attackSeconds;
        state = AttackState.CHARGING;
        chargeDuration = chargeSeconds;
        attackDuration = attackSeconds;
    }

    // Action is called once per frames
    public override void Action()
    {
        duration -= Time.deltaTime;

        
        if (chargeDuration > 0)
        {
            Debug.Log("charging");
            chargeDuration -= Time.deltaTime;
            return;
        }

        Vector2 hazardPosition = new Vector2(0, 0);

        switch (state)
        {
            case AttackState.CHARGING:
                Debug.Log("chargeDone");
                state = AttackState.ATTACK;
                // create meateor
                hazardPosition = Attack();
                break;
            case AttackState.ATTACK:
                // Should be currently moving to the ground
                break;
            case AttackState.STAGE_HAZARD:
                state = AttackState.DONE;
                
                if (attackDuration > 0)
                {
                    attackDuration -= Time.deltaTime;
                    break;
                }
                if (hazardPosition.x != 0f)
                    Instantiate(stageHazard, hazardPosition, Quaternion.identity);
                break;
            case AttackState.DONE:
                // Should be currently moving up
                break;

            default:
                Debug.LogError("Attack State is not CHARGING, ATTACK, STAGE_HAZARD or DONE: " + state);
                break;
        }
    }

    private Vector2 Attack()
    {
        Debug.Log("attacking");

        // calculate the positions for the stage hazard (left and right side of screen)
        Vector2[] targetPositions = new Vector2[2];
        Camera mainCamera = Camera.main;
        float topOfGroundHeight = groundTransform.position.y + groundTransform.localScale.y / 2f;
        float bulletHeight = groundTransform.position.y + groundTransform.localScale.x / 2f;
        float stageHazardWidth = stageHazard.GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log("hazard width: " + stageHazardWidth);
        float screenWidth = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x;

        // Calculate the left and right bounds of the screen
        float leftBound = mainCamera.transform.position.x - (screenWidth / 2f) + (stageHazardWidth / 2f);
        float rightBound = mainCamera.transform.position.x + (screenWidth / 2f) - (stageHazardWidth / 2f);
        targetPositions[0] = new Vector2(leftBound, topOfGroundHeight + bulletHeight);
        targetPositions[1] = new Vector2(rightBound, topOfGroundHeight + bulletHeight);

        Debug.Log("Left Bound: " + leftBound);
        Debug.Log("Right Bound: " + rightBound);

        // create the meateor at the boss' y position but with a random x position
        float bulletWidth = bulletPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float randomX = UnityEngine.Random.Range(((float)Screen.width / -2) + bulletWidth, ((float)Screen.width / -2) - bulletWidth);
        Vector2 initialPosition = new Vector2(randomX, transform.position.y);
        Vector2 targetPosition = targetPositions[UnityEngine.Random.Range(0, 1)];
        Vector2 direction = new Vector2(targetPosition.x - initialPosition.x, targetPosition.y - initialPosition.y).normalized; // get rotation while keeping scale to 1
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        GameObject bullet = Instantiate(bulletPrefab, initialPosition, rotation);
        float distanceToTargetPosition = Vector2.Distance(initialPosition, targetPosition);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript == null)
            Debug.LogError("bulletScript not found");
        bulletScript.speed = distanceToTargetPosition / attackSeconds; // set the speed of meateor
        Debug.Log(bulletScript.speed);
        DestroyOnTime bulletDestroyScript = bullet.GetComponent<DestroyOnTime>();
        if (bulletDestroyScript == null)
            Debug.LogError("bulletDestroyScript not found");
        bulletDestroyScript.TotalLifeSeconds = attackSeconds;

        state = AttackState.STAGE_HAZARD;
        return new Vector2(targetPosition.x, targetPosition.y - bulletHeight);
    }
}