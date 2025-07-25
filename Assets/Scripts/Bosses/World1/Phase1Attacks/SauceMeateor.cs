using System;
using UnityEngine;

public class SauceMeateor : BossAction
{
    [SerializeField] private float chargeSeconds;
    [SerializeField] private float attackSeconds; // time for the meateor to hit the ground after being made
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject stageHazard;
    [SerializeField] private Transform groundTransform; // the position of the ground
    [SerializeField] private float hazardSeconds;
    [SerializeField] private bool hazardTimeAffectsTotalActionDuration;
    [SerializeField] private float leftEdge; //left and right screen boundaries set to -7.5 and 7.5 respectively, 3/5/2024
    [SerializeField] private float rightEdge;

    private Vector2 targetPosition;
    private float bulletHeight;
    private float chargeDuration;
    private float attackDuration;
    private Vector2 hazardPosition;

    private enum AttackState
    {
        CHARGING = 0, ATTACK = 1, STAGE_HAZARD = 2, DONE
    }

    private AttackState state;

    // BeginAction is called at the start of this script
    public override void BeginAction()
    {
        duration = chargeSeconds + attackSeconds;
        duration += (hazardTimeAffectsTotalActionDuration) ? hazardSeconds : 0.1f;
        state = AttackState.CHARGING;
        chargeDuration = chargeSeconds;
        attackDuration = attackSeconds;
        hazardPosition = new Vector2(0, 0);
    }

    // Action is called once per frames
    public override void Action()
    {
        duration -= Time.deltaTime;

        if (chargeDuration > 0)
        {
            chargeDuration -= Time.deltaTime;
            return;
        }

        switch (state)
        {
            case AttackState.CHARGING:
                state = AttackState.ATTACK;
                // create meateor
                Attack();
                break;
            case AttackState.ATTACK:
                // meateor should be currently moving to the ground
                if (attackDuration > 0)
                {
                    attackDuration -= Time.deltaTime;
                    break;
                }
                state = AttackState.STAGE_HAZARD;
                break;
            case AttackState.STAGE_HAZARD:
                if (hazardPosition.x != 0f)
                {
                    GameObject hazard = Instantiate(stageHazard, hazardPosition, Quaternion.identity);
                    DestroyAfterTime hazardDestroyScript = hazard.GetComponent<DestroyAfterTime>();
                    if (hazardDestroyScript == null)
                        Debug.LogError("bulletDestroyScript not found");
                    else
                        hazardDestroyScript.TotalLifeSeconds = hazardSeconds;
                }
                state = AttackState.DONE;
                break;
            case AttackState.DONE:
                break;

            default:
                Debug.LogError("Attack State is not CHARGING, ATTACK, STAGE_HAZARD or DONE: " + state);
                break;
        }
    }

    private void Attack()
    {
        // calculate the positions for the stage hazard (left and right side of screen)
        Vector2[] targetPositions = new Vector2[2];
        Camera mainCamera = Camera.main;
        float topOfGroundHeight = groundTransform.position.y + groundTransform.localScale.y / 2f;
        float bulletHeight = groundTransform.position.y + groundTransform.localScale.x / 2f;
        float stageHazardWidth = stageHazard.GetComponent<SpriteRenderer>().bounds.size.x;
        float screenWidth = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        float screenHeight = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y - mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y;

        // Calculate the left and right bounds of the screen. make sure it doesn't go out the play field
        float leftBound = mainCamera.transform.position.x - (screenWidth / 2f) + (stageHazardWidth / 2f);
        float rightBound = mainCamera.transform.position.x + (screenWidth / 2f) - (stageHazardWidth / 2f);
        
        leftBound = (leftBound < (leftEdge + (stageHazardWidth / 2f))) ? (leftEdge + (stageHazardWidth / 2f)) : leftBound;
        rightBound = (rightBound > (rightEdge - (stageHazardWidth / 2f))) ? (rightEdge - (stageHazardWidth / 2f)) : rightBound;
        
        //Calculate positions for the meateor to land (on top of ground and either on the left or right side)
        targetPositions[0] = new Vector2(leftBound, groundTransform.position.y + topOfGroundHeight / 2f + bulletHeight / 2f);
        targetPositions[1] = new Vector2(rightBound, groundTransform.position.y + topOfGroundHeight / 2f + bulletHeight / 2f);

        // create the meateor at the top of the screen and with a random x position and calculate its speed and direction
        float bulletWidth = bulletPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        float randomX = UnityEngine.Random.Range(leftBound + bulletWidth, rightBound - bulletWidth);
        Vector2 initialPosition = new Vector2(randomX, screenHeight / 2f);
        Vector2 targetPosition = targetPositions[UnityEngine.Random.Range(0, 2)];
        Vector2 direction = new Vector2(targetPosition.x - initialPosition.x, targetPosition.y - initialPosition.y).normalized; // get rotation while keeping scale to 1
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        GameObject bullet = Instantiate(bulletPrefab, initialPosition, rotation);
        float distanceToTargetPosition = Vector2.Distance(initialPosition, targetPosition);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript == null)
            Debug.LogError("bulletScript not found");
        else
            bulletScript.speed = distanceToTargetPosition / attackSeconds;
        DestroyAfterTime bulletDestroyScript = bullet.GetComponent<DestroyAfterTime>();
        if (bulletDestroyScript == null)
            Debug.LogError("bulletDestroyScript not found");
        else
            bulletDestroyScript.TotalLifeSeconds = attackSeconds;

        hazardPosition = new Vector2(targetPosition.x, targetPosition.y);
    }

    /*
    // delegated event for the boss's damage receive sound sounds
    public delegate void BossDamageSoundEventHandler();
    public event BossDamageSoundEventHandler OnBossDamageSoundEvent;
    */
}