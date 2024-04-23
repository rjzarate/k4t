using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSpray : BossAction
{

    [Header("Attack Attributes")]
    [SerializeField] float rateOfFireTime = 0.5f;
    float rateOfFireCooldown;
    [SerializeField] float bulletSpeed;

    [Header("Note: Position and Direction should have the same length,\ndon't worry about the errors\n")]
    [SerializeField] Vector2[] bulletPosition;
    [SerializeField] float[] bulletRotation;
    [SerializeField] GameObject bulletPrefab;

    [Header("Spread - can be manipulated via another script")]
    public float[] appliedRotation;

    [Header("Ray Cast:")]
    [SerializeField] float rayCastDistance;
    public enum rayCastColors { red, yellow, pink, cyan, blue }
    [SerializeField] rayCastColors rayColor;

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

        for ( int i = 0; i < appliedRotation.Length; i++)
        {
            bulletRotation[i] += Time.deltaTime * appliedRotation[i];
        }

        rateOfFireCooldown -= Time.deltaTime;
    }


    private void FireBullet()
    {
        // instantiate and reference the bullet
        Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);

        for ( int i = 0; i < bulletPosition.Length; i++)
        {
            float internalRot;

            // rotation
            if (bulletRotation.Length >= bulletPosition.Length)
            {
                internalRot = bulletRotation[i];
            }

            else {
                internalRot = bulletRotation[bulletRotation.Length];
            }

            Vector3 angle = new Vector3(0, 0, internalRot);
            Vector3 rayDirection = transform.TransformDirection(angleToDir(angle)) * rayCastDistance;
            Instantiate(bulletPrefab, transformPosition + bulletPosition[i], Quaternion.LookRotation(Vector3.forward, rayDirection));
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
    }

    private void OnDrawGizmosSelected()
    {
        if (!showGizmos)
        {
            return;
        }

        // Display the ray of where the boss will fire the bullet
        Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);


        // picking color for a more clear coordination
        switch (rayColor)
        {
            case rayCastColors.red:
                Gizmos.color = Color.red;
                break;
            case rayCastColors.yellow:
                Gizmos.color = Color.yellow;
                break;
            case rayCastColors.pink:
                Gizmos.color = Color.magenta;
                break;
            case rayCastColors.cyan:
                Gizmos.color = Color.cyan;
                break;
            case rayCastColors.blue:
                Gizmos.color = Color.blue;
                break;
        }
        
        
        for (int i = 0; i < bulletPosition.Length; i++)
        {
            Vector3 rayRot;

            if (bulletRotation.Length >= bulletPosition.Length)
            {
                rayRot = new Vector3(0, 0, bulletRotation[i]);
            }

            else {
                rayRot = new Vector3(0, 0, bulletRotation[0]);
            }

            Vector3 rayRirection = transform.TransformDirection(angleToDir(rayRot)) * rayCastDistance;
            Gizmos.DrawRay(transformPosition + bulletPosition[i], rayRirection * rayCastDistance);
        }
    }


    // convert angle to direction using trig
    Vector2 angleToDir( Vector3 getRot )
    {
        float angle = getRot.z * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        return new Vector2(x, y);
    }
}