using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackRain : BossAction
{
    [Header("Attack Attributes")]
    [SerializeField] float rateOfFireTime = 0.5f;
    float rateOfFireCooldown;
    [SerializeField] float bulletSpeed;

    [Header("Note: Position and Direction should have the same length,\ndon't worry about the errors\n")]
    [SerializeField] Vector2 bulletPosition;
    [SerializeField] Vector2 summonRange;

    [SerializeField] float globalRotation;
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
            Quaternion rotation = Quaternion.AngleAxis(globalRotation, Vector3.forward);

            Vector2 rangePosition = new Vector2(Random.Range(-summonRange.x, summonRange.x), Random.Range(-summonRange.y, summonRange.y));
            GameObject bullet = Instantiate(bulletPrefab, transformPosition + bulletPosition + rangePosition, rotation);

            // if the bullet as the bullet script, then set the speed to the boss's bullet speed
            if (bullet.GetComponent<Bullet>())
            {
                bullet.GetComponent<Bullet>().speed = bulletSpeed;
            }
        }

    }

    public override void BeginAction()
    {
        duration = time;
        rateOfFireCooldown = rateOfFireTime;

        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
}
