using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackWhip : BossAction
{

    [Header("Attack Attributes")]
    [SerializeField] float rateOfFireTime = 0.5f;
    float rateOfFireCooldown;
    [SerializeField] float bulletSpeed;

    [Header("Note: Position and Direction should have the same length,\ndon't worry about the errors\n")]
    [SerializeField] Vector2 bulletPosition;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] GameObject playerObj;

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


        if (playerObj != null)
        {
            Vector3 direction = playerObj.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            Instantiate(bulletPrefab, transformPosition + bulletPosition, rotation);
        }


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

        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
}
