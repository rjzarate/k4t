using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System;
using UnityEngine.Rendering;

public class Boss : MonoBehaviour
{

    // Controlling the states
    // the times for the states: idle, atk, damaged, and death

    [SerializeField] private List<BossAction> actions = new List<BossAction>();
    [SerializeField] private BossAction currentAction;



    /* DEPRECATED
    // special effects and boss animation that can be added
    [Header("Effects:")]
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject attackFX;

    [SerializeField] Animator bossAnimation;
    */



    void Start()
    {
        // If action is not set in inspector
        if (currentAction == null) {
            Debug.LogWarning("No current action set. Setting to first Action in the list: " + actions[0]);
            currentAction = actions[0];
        }
        currentAction.BeginAction();
    }

    void Update()
    {
        // Continuously play the current action
        if (!currentAction.ActionFinished()) {
            currentAction.Action();
            return;
        }

        // Action is finished, generate a new action //
        BossAction newAction = GetNewAction();
        Debug.Log(newAction);
        currentAction = newAction;
        currentAction.BeginAction();
        return;
    }

    // Depends on current action's conditions:
    // 1. whether it forces an action
    // 2. blacklists
    private BossAction GetNewAction()
    {
        // If the current action forces the next action
        if (currentAction.WillForceNextAction()) {
            return currentAction.GetNextForcedAction();
        }

        // Else, just do a random action 
        // Grabs total weight of all nonblacklisted actions 
        int totalWeight = 0;

        foreach (BossAction action in actions.FindAll(a => (a != currentAction && !currentAction.getBlacklistedActions().Contains(a))
                                                        || (a == currentAction && !a.BlacklistsSelf()))) {
            totalWeight += action.GetWeight();
        }

        // Roll for the action
        /* IF WE DOING SEEDED STUFF, THIS SHOULD BE SET BY THE CONSTANT SEED */
        System.Random random = new System.Random();
        int roll = random.Next(totalWeight); // exclusive
        Debug.Log("Roll: " + roll);

        // Grab the random action
        List<BossAction> nonBlacklistedActions = actions.FindAll(a => (a != currentAction && !currentAction.getBlacklistedActions().Contains(a))
                                                                   || (a == currentAction && !a.BlacklistsSelf()));
        BossAction previousAction = nonBlacklistedActions[0];
        foreach (BossAction action in nonBlacklistedActions) {
            // Sifts through the actions' weights
            roll -= previousAction.GetWeight();
            previousAction = action;
            if (roll > 0) {
                continue;
            }
            break;
        }

        return previousAction;
    }

    /* DEPRECATED
    // idle time
    public void Idle()
    {
        bossAnimation.SetInteger("state", (int) state);
        // idleDur = StateDuration(idleTime, idleDur, BossState.ATTACK1);
    }


    // damaged time, when the boss flinches or backs down
    public void Damaged()
    {
        bossAnimation.SetTrigger("damaged");
        // damagedDur = StateDuration(damagedTime, damagedDur, BossState.IDLE);
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
    */
}