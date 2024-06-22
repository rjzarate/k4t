using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerAttack), typeof(PlayerMovement), typeof(Health))]
public class Player : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private Health playerHealth;

    public static Player Instance { get; private set; }

    public void Awake()
    {
        Instance = this;

        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<Health>();
    }

    public PlayerAttack GetPlayerAttack() {
        return playerAttack;
    }

    public PlayerMovement GetPlayerMovement() {
        return playerMovement;
    }

    public Health GetPlayerHealth() {
        return playerHealth;
    }


}
