using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GratedCheeseRain : BossAction
{

    [Header("Attack Attributes")]
    [SerializeField] private float rateOfFireTime = 0.2f;
    private float rateOfFireCooldown;
    [SerializeField] private float bulletAccel;
    [SerializeField] private float width;
    [SerializeField] private float maxHorizontalVelocity;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float movementSpeed;

    // REMOVE THIS LATER
    [SerializeField] private float targetX;

    public override void Action()
    {
        Attack();
        duration -= Time.deltaTime;
    }

    private void Attack()
    {
        if (rateOfFireCooldown < 0)
        {
            Fire();
            rateOfFireCooldown = rateOfFireTime;
            return;
        }
        rateOfFireCooldown -= Time.deltaTime;
        // CHANGE targetX TO PLAYER'S X POSITION
        float positionalDifference = targetX - transform.position.x;
        if (Math.Abs(positionalDifference) > 0)
        {
            float movement = movementSpeed * Time.deltaTime;
            if (Math.Abs(positionalDifference) < movement)
            {
                // CHANGE targetX TO PLAYER'S X POSITION
                transform.position = new Vector2(targetX, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x + Math.Sign(positionalDifference) * movement, transform.position.y);
            }
        }
    }


    private void Fire()
    {
        Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);
        GameObject bullet = Instantiate(bulletPrefab, transformPosition + new Vector2(UnityEngine.Random.Range(-width / 2, width / 2), 0), Quaternion.identity);

        // if the bullet has the bullet script, then set the speed to the boss's bullet speed
        if (bullet.GetComponent<AcceleratingBullet>())
        {
            bullet.GetComponent<AcceleratingBullet>().Initialize(new Vector2(UnityEngine.Random.Range(-maxHorizontalVelocity, maxHorizontalVelocity), 0), new Vector2(0, -bulletAccel));
        }
    }

    public override void BeginAction()
    {
        duration = time;
        rateOfFireCooldown = rateOfFireTime;
    }

    private void OnDrawGizmosSelected() {

        if (!showGizmos) {
            return;
        }

        // Display the ray of where the boss will fire the bullet
        
        //    Gizmos.DrawRay(transformPosition, direction);
        
    }
}
