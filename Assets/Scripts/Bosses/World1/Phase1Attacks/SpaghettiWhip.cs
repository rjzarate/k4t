using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaghettiWhip : BossAction
{
    public static SpaghettiWhip Instance { get; private set; }


    [Header("Attack Attributes")]
    [SerializeField] float rateOfFireTime = 0.5f;
    float rateOfFireCooldown;
    [SerializeField] float bulletSpeed;

    [Header("Note: Position and Direction should have the same length,\ndon't worry about the errors\n")]
    [SerializeField] Vector2 bulletPosition;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float delaySeconds;
    private float delayLeftSeconds;

    [SerializeField] private Vector3 playerPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public override void Action()
    {
        if (delayLeftSeconds > 0)
        {
            delayLeftSeconds -= Time.deltaTime;
        }
        else
        {
            Attack();
        }
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


        if (playerPosition != null)
        {
            Vector3 direction = playerPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            GameObject bullet = Instantiate(bulletPrefab, transformPosition + bulletPosition, rotation);

            // if the bullet as the bullet script, then set the speed to the boss's bullet speed
            if (bullet.GetComponent<Bullet>())
            {
                bullet.GetComponent<Bullet>().speed = bulletSpeed;
            }

            // plays the sound for the boss's whip attack
            OnBossAttackWhipSoundEvent();
        }
    }

    // delegated event for the boss's whip attack sound
    public delegate void BossAttackWhipSoundEventHandler();
    public event BossAttackWhipSoundEventHandler OnBossAttackWhipSoundEvent;

    public override void BeginAction()
    {
        delayLeftSeconds = delaySeconds;
        duration = time;
        rateOfFireCooldown = rateOfFireTime;

        playerPosition = Player.Instance.transform.position;
    }


}
