using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(BossController))]
public class Boss : MonoBehaviour
{
    public static Boss Instance { get; private set; }
    private Health bossHealth;
    private BossController bossController;

    public void Awake()
    {
        Instance = this;

        bossHealth = GetComponent<Health>();
        bossController = GetComponent<BossController>();
    }

    public Health GetBossHealth() {
        return bossHealth;
    }

    public List<BossAction> GetBossActions() {
        return bossController.GetActions();
    }
}
