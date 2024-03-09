using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseWheel : BossAction
{

    [Header("Attack Attributes")]
    [SerializeField] private float rateOfFireTime = 0.2f;
    private float rateOfFireCooldown;
    [SerializeField] private float bulletSpeed = 7;
    // the "n" value on the game design specification; the attack will shoot 2n+1 or 2n bullets each wave
    [SerializeField] private int nValue = 4;
    [SerializeField] private GameObject bulletPrefab;
    private bool wave2 = false;

    public override void Action()
    {
        Attack();
        duration -= Time.deltaTime;
    }

    private void Attack()
    {
        if (rateOfFireCooldown < 0)
        {
            if (!wave2)
            {
                FireWave1();
                wave2 = true;
            }
            else
            {
                FireWave2();
                wave2 = false;
            }
            rateOfFireCooldown = rateOfFireTime;
            return;
        }
        rateOfFireCooldown -= Time.deltaTime;

    }


    private void FireWave1()
    {
        // instantiate and reference the bullet
        for (int i = 0; i <= 2 * nValue; i++)
        {
            // note that -90 is added to the game design specifications because the game design specifications are relative to the rightward direction, while the bullet here is relative to the upward direction
            Quaternion bulletRotation = Quaternion.Euler(0, 0, -90 - ((90f / nValue) * i));
            Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);
            GameObject bullet = Instantiate(bulletPrefab, transformPosition, bulletRotation);

            // if the bullet has the bullet script, then set the speed to the boss's bullet speed
            if (bullet.GetComponent<Bullet>())
            {
                bullet.GetComponent<Bullet>().speed = bulletSpeed;
            }
        }
    }

    private void FireWave2()
    {
        // instantiate and reference the bullet
        for (int i = 0; i < 2 * nValue; i++)
        {
            // note that -90 is added to the game design specifications because the game design specifications are relative to the rightward direction, while the bullet here is relative to the upward direction
            Quaternion bulletRotation = Quaternion.Euler(0, 0, -90 - (45f / nValue) - ((90f / nValue) * i));
            Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);
            GameObject bullet = Instantiate(bulletPrefab, transformPosition, bulletRotation);

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
    }

    private void OnDrawGizmosSelected() {

        if (!showGizmos) {
            return;
        }

        // Display the ray of where the boss will fire the bullet
        Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);
        bool isWave2 = false;
        for (int i = 0; i <= 4 * nValue; i++)
        {
            Color c1 = new Color(1, 0.9f, 0.3f);
            Color c2 = new Color(0.5f, 0.45f, 0.15f);
            if (!isWave2)
            {
                Gizmos.color = c1;
                isWave2 = true;
            }
            else
            {
                Gizmos.color = c2;
                isWave2 = false;
            }
            // total life seconds of the bullet
            float totalLifeSeconds = 2;
            Vector2 direction = (Quaternion.Euler(0, 0, -90 - ((45f / nValue) * i)) * Vector3.up) * bulletSpeed * totalLifeSeconds;
            Gizmos.DrawRay(transformPosition, direction);
        }
        
    }
}
