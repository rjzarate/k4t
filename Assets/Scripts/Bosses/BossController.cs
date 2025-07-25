using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System;
using UnityEngine.Rendering;

public class BossController : MonoBehaviour
{

    // Controlling the states
    // the times for the states: idle, atk, damaged, and death

    [SerializeField] private List<BossAction> actions = new List<BossAction>();
    [SerializeField] private BossAction currentAction;


    private void Start()
    {
        // If action is not set in inspector
        if (currentAction == null) {
            Debug.LogWarning("No current action set. Setting to first Action in the list: " + actions[0]);
            currentAction = actions[0];
        }
        currentAction.BeginAction();
    }

    private void Update()
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
        BossAction selectedAction = null;
        foreach (BossAction action in nonBlacklistedActions) {
            selectedAction = action;
            // Sifts through the actions' weights
            roll -= selectedAction.GetWeight();
            if (roll > 0) {
                continue;
            }
            break;
        }

        return selectedAction;
    }

    public List<BossAction> GetActions() {
        return actions;
    }
}