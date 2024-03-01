using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : BossAction
{

    [Header("Attack Attributes")]
    [SerializeField] private float rateOfFireTime = 0.2f;
    private float rateOfFireCooldown;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Vector2 bulletPosition;
    [SerializeField] private Vector2 bulletPositionRelativeDirection;
    [SerializeField] private GameObject bulletPrefab;

    public override void Action()
    {
        Attack();
        duration -= Time.deltaTime;
    }

    private void Attack()
    {
        if (rateOfFireCooldown < 0)
        {
            FireBullet();
            rateOfFireCooldown = rateOfFireTime;
            return;
        }

        rateOfFireCooldown -= Time.deltaTime;
        
    }


    private void FireBullet()
    {
        // instantiate and reference the bullet
        Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, bulletPositionRelativeDirection);
        Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);
        Instantiate(bulletPrefab, transformPosition + bulletPosition, bulletRotation);

        // if the bullet as the bullet script, then set the speed to the boss's bullet speed
        if (GetComponent<Bullet>())
        {
            bulletPrefab.GetComponent<Bullet>().speed = bulletSpeed;
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
        Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transformPosition + bulletPosition, bulletPositionRelativeDirection);
    }
}
