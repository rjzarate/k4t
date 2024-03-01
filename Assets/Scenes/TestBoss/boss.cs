using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Boss : MonoBehaviour, IBoss, IBullet
{

    private enum BossState {
        IDLE = 0, ATTACK1 = 1, DAMAGE = 2, DEATH
    }
    // Controlling the states
    // the times for the states: idle, atk, damaged, and death

    [Header("States:")]
    [SerializeField] BossState state = BossState.IDLE;

    // static time. The total time of each state
    [SerializeField] float idleTime;
    [SerializeField] float atkTime;
    [SerializeField] float damagedTime;
    [SerializeField] float deathTime;

    // the duration time that will be changed over time
    float idleDur;
    float atkDur;
    float damagedDur;


    // bullet handler
    // firing position, bullet gameobject, speed of the bullet, and fps time
    [Header("Bullets:")]
    [SerializeField] Transform firePos;
    [SerializeField] GameObject bullets;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fpsTime;
    float fpsDur;


    // special effects and boss animation that can be added
    [Header("Effects:")]
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject attackFX;

    [SerializeField] Animator bossAnimation;



    // if there is animation setup, then set bossAnim variable to that existing component
    // initial setters for the durations
    void Start()
    {
        if (GetComponent<Animator>())
        {
            bossAnimation = GetComponent<Animator>();
        }

        idleDur = idleTime;
        atkDur = atkTime;
        damagedDur = damagedTime;
        fpsDur = 0;
    }

    void Update()
    {
        // this line will be useful later on through later implementations:
        // bossAnim.SetInteger("state", state);


        // the many states, which actions are in functions
        switch (state) {
            case BossState.IDLE:
            Idle();
            break;

            case BossState.ATTACK1:
            Attack();
            break;

            case BossState.DAMAGE:
            Damaged();
            break;

            default:
            Death();
            break;

        }
    }


    // idle time
    public void Idle()
    {
        bossAnimation.SetInteger("state", (int) state);
        idleDur = StateDuration(idleTime, idleDur, BossState.ATTACK1);
    }

    // attack time
    public void Attack()
    {
        bossAnimation.SetInteger("state", (int) state);


        atkDur = StateDuration(atkTime, atkDur, BossState.IDLE);
        // fps time
        if (fpsDur <= 0)
        {
            FireBullet();
            fpsDur = fpsTime;
        }

        else {
            fpsDur -= Time.deltaTime;
        }
    }


    // firing bullet
    public void FireBullet()
    {
        // instantiate and reference the bullet
        GameObject bulletPrefab = Instantiate(bullets, firePos.position, firePos.rotation);

        // if the bullet as the bullet script, then set the speed to the boss's bullet speed
        if (GetComponent<Bullet>())
        {
            bulletPrefab.GetComponent<Bullet>().speed = bulletSpeed;
        }
    }



    // damaged time, when the boss flinches or backs down
    public void Damaged()
    {
        bossAnimation.SetTrigger("damaged");
        damagedDur = StateDuration(damagedTime, damagedDur, BossState.IDLE);
    }

    // this is the time when the boss dies
    // instantiates death effect once dead
    public void Death()
    {
        bossAnimation.SetInteger("state", (int) state);
        if (deathTime <= 0)
        {
            Instantiate(deathFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        else {
            deathTime -= Time.deltaTime;
        }
    }




    // this function is called to countdown, then switch to the next selected transition state
    float StateDuration(float totalTime, float duration, BossState transitionState)
    {
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            state = transitionState;
            duration = totalTime;
        }

        return duration;
    }
}