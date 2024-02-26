using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class boss : MonoBehaviour
{
    [Header("States:")]
    [SerializeField] int state = 0;


    [SerializeField] float idleTime;
    [SerializeField] float atkTime;
    [SerializeField] float damagedTime;
    [SerializeField] float deathTime;

    float idleDur;
    float atkDur;
    float damagedDur;

    [Header("Bullets:")]
    [SerializeField] Transform firePos;
    [SerializeField] GameObject bullets;
    [SerializeField]float bulletSpeed;
    [SerializeField] float fpsTime;
    float fpsDur;


    [Header("Effects:")]
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject attackFX;

    [SerializeField] Animator bossAnim;



    void Start()
    {
        if (GetComponent<Animator>())
        {
            bossAnim = GetComponent<Animator>();
        }

        idleDur = idleTime;
        atkDur = atkTime;
        damagedDur = damagedTime;
        fpsDur = 0;
    }

    void Update()
    {
        //bossAnim.SetInteger("state", state);

        if (state == 0)
        {
            idle();
        }

        else if (state == 1)
        {
            attack();
        }

        else if (state == 2)
        {
            damaged();
        }

        else {
            death();
        }
    }

    void idle()
    {
        //bossAnim.SetInteger("state", state);
        idleDur = stateDuration(idleTime, idleDur, 1);
    }

    void attack()
    {
        //bossAnim.SetInteger("state", state);
        atkDur = stateDuration(atkTime, atkDur, 0);

        if (fpsDur <= 0)
        {
            GameObject bulletPrefab = Instantiate(bullets, firePos.position, firePos.rotation);

            if (GetComponent<bullet>())
            {
                bulletPrefab.GetComponent<bullet>().speed = bulletSpeed;
            }

            fpsDur = fpsTime;
        }

        else {
            fpsDur -= Time.deltaTime;
        }
    }

    void damaged()
    {
        //bossAnim.SetTrigger("damaged");
        damagedDur = stateDuration(damagedTime, damagedDur, 0);
    }

    void death()
    {
        //bossAnim.SetInteger("state", state);
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject, deathTime);
    }


    float stateDuration( float totalTime, float duration, int transitionState)
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