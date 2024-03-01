using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAction : MonoBehaviour
{
    [Header("Base Attributes")]
    [SerializeField] protected float time = 1f;
    [SerializeField] protected int weight = 10;
    protected float duration;

    [Header("Forcing Action")]
    [SerializeField] protected bool willForceNextAction = false;
    [SerializeField] protected BossAction nextForcedAction;
    
    [Header("Blacklisted Actions")]
    [SerializeField] protected List<BossAction> blacklistedActions = new List<BossAction>();
    [SerializeField] protected bool blacklistsSelf = true;

        [Header("Debug")]
    [SerializeField] protected bool showGizmos = true;



    private void Start() {
        duration = time;
    }

    abstract public void BeginAction();
    abstract public void Action();


    public float GetTime() {
        return time;
    }

    public float GetDuration() {
        return duration;
    }

    public bool ActionFinished() {
        return duration < 0;
    }

    public int GetWeight() {
        return weight;
    }

    public bool WillForceNextAction() {
        return willForceNextAction;
    }

    public BossAction GetNextForcedAction() {
        return nextForcedAction;
    }

    public List<BossAction> getBlacklistedActions() {
        return blacklistedActions;
    }

    public bool BlacklistsSelf() {
        return blacklistsSelf;
    }
}
