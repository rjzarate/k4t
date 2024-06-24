using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GridBrushBase;

public class MouseMafia : BossAction
{
    [Header("Attack Attributes")]
    [SerializeField] private float rateOfFireTime = 0.2f;
    private float rateOfFireCooldown;
    // bulletCount represents how many bullets will be fired whenever the boss pauses
    [SerializeField] private int bulletCount = 3;
    [SerializeField] private float bulletSpeed = 7f;
    private int bulletsLeft;
    // Boss is either moving, attacking, or in the middle of a delay between moving and attacking
    private enum State {MOVE, ATTACK, DELAY};
    private State currState = State.MOVE;
    // Delay between moving and attacking
    [SerializeField] private float attackDelay;
    private float currDelay = 0f;
    // When this attack ends, the boss needs to return to its default position
    private bool durationEnded = false;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float moveTime;
    private float movementSpeed;

    // DEPRECATED
    // Where the boss will aim at (Will be the player's x value later)
    // [SerializeField] private float playerX = 0, playerY = -3.14f;

    [Header("Boss Movement Range")]
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;

    // Where the boss will move to after firing its bullets - Must be within the given range
    private float targetX, targetY;

    // Boss return position
    [SerializeField] private Vector2 returnPosition;

    public override void Action()
    {
        if (currState == State.ATTACK)
        {
            Attack();
        }
        else if(currState == State.DELAY)
        {
            currDelay -= Time.deltaTime;
            if (currDelay <= 0f)
                currState = State.ATTACK;
        }
        else if (!durationEnded)
        {
            Move();
        }
        
        // When the attack ends, return to the default position
        if (duration < Time.deltaTime)
        {
            durationEnded = true;
            currState = State.MOVE;
            // Default position
            targetX = returnPosition.x;
            targetY = returnPosition.y;

            Move();

            // if we reached the default mosition
            if(currState == State.DELAY)
            {
                duration = -1;
            }
            return;
        }

        duration -= Time.deltaTime;
    }

    private void Attack()
    {
        if (rateOfFireCooldown < 0)
        {
            Fire();
            if (bulletsLeft == 0)
            {
                // Get ready to start Move()-ing if we've ran out of bullets
                currState = State.MOVE;
                targetX = UnityEngine.Random.Range(minX, maxX);
                targetY = UnityEngine.Random.Range(minY, maxY);
                // Calculate speed that will allow a constant travel time
                movementSpeed = Mathf.Sqrt(Mathf.Pow(targetX - transform.position.x, 2) + Mathf.Pow(targetY - transform.position.y, 2)) / moveTime;
            }
            rateOfFireCooldown = rateOfFireTime;
            return;
        }
        rateOfFireCooldown -= Time.deltaTime;
    }

    private void Fire()
    {
        // Get player position
        float playerX = Player.Instance.transform.position.x;
        float playerY = Player.Instance.transform.position.y;

        float rotationDirection;
        if (transform.position.x - playerX == 0)
            rotationDirection = -180f;
        else
            rotationDirection = Mathf.Abs(transform.position.x - playerX) / (transform.position.x - playerX);
        // Instantiate and reference the bullet
        Quaternion bulletRotation = Quaternion.Euler(0, 0, rotationDirection * 90 + Mathf.Atan((playerY - transform.position.y)/(playerX - transform.position.x)) * 180f/Mathf.PI);
        Vector3 transformPosition = new Vector3(transform.position.x, transform.position.y, 0);
        GameObject bullet = Instantiate(bulletPrefab, transformPosition, bulletRotation);

        // If the bullet has the bullet script, then set the speed of the bullet to the boss's bullet speed
        if (bullet.GetComponent<Bullet>())
        {
            bullet.GetComponent<Bullet>().speed = bulletSpeed;
        }

        bulletsLeft--;
    }

    private void Move()
    {
        if (transform.position.x == targetX && transform.position.y == targetY)
        {

            // Get ready to pause before Attack()-ing if the boss has finished moving
            currState = State.DELAY;
            currDelay = attackDelay;
            bulletsLeft = bulletCount;
            return;
        }
        Vector3 movementDirection = (new Vector3(targetX - transform.position.x, targetY - transform.position.y, 0)).normalized;
        float newXValue = transform.position.x + movementDirection.x * movementSpeed * Time.deltaTime;

        if ((newXValue > targetX && movementDirection.x > 0) || (newXValue < targetX && movementDirection.x < 0))
        {
            transform.position = new Vector3(targetX, targetY, 0);
            return;
        }
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }

    public override void BeginAction()
    {
        duration = time;
        durationEnded = false;
        rateOfFireCooldown = rateOfFireTime;
        targetX = UnityEngine.Random.Range(minX, maxX);
        targetY = UnityEngine.Random.Range(minY, maxY);

        movementSpeed = Mathf.Sqrt(Mathf.Pow(targetX - transform.position.x, 2) + Mathf.Pow(targetY - transform.position.y, 2)) / moveTime;
    }

    private void OnDrawGizmosSelected()
    {

        if (!showGizmos)
        {
            return;
        }

        // Display the ray of where the boss will fire the bullet

        //    Gizmos.DrawRay(transformPosition, direction);

    }
}
