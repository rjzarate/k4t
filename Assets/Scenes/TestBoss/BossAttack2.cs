using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2 : BossAction
{

    [Header("Attack Attributes")]
    [SerializeField] private float rateOfFireTime = 0.5f;
    private float rateOfFireCooldown;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Vector2 bulletPosition1;
    [SerializeField] private Vector2 bulletPositionRelativeDirection1;
    [SerializeField] private Vector2 bulletPosition2;
    [SerializeField] private Vector2 bulletPositionRelativeDirection2;
    [SerializeField] private Vector2 bulletPosition3;
    [SerializeField] private Vector2 bulletPositionRelativeDirection3;
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
        Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);
        
        Quaternion bulletRotation1 = Quaternion.LookRotation(Vector3.forward, bulletPositionRelativeDirection1);
        Instantiate(bulletPrefab, transformPosition + bulletPosition1, bulletRotation1);

        Quaternion bulletRotation2 = Quaternion.LookRotation(Vector3.forward, bulletPositionRelativeDirection2);
        Instantiate(bulletPrefab, transformPosition + bulletPosition2, bulletRotation2);

        Quaternion bulletRotation3 = Quaternion.LookRotation(Vector3.forward, bulletPositionRelativeDirection2);
        Instantiate(bulletPrefab, transformPosition + bulletPosition3, bulletRotation3);
        

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

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transformPosition + bulletPosition1, bulletPositionRelativeDirection1);
        Gizmos.DrawRay(transformPosition + bulletPosition2, bulletPositionRelativeDirection2);
        Gizmos.DrawRay(transformPosition + bulletPosition3, bulletPositionRelativeDirection3);
    }
}
