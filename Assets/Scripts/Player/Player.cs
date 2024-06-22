using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerAttack), typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private static PlayerAttack playerAttack;
    private static PlayerMovement playerMovement;

    public static Player Instance { get; private set; }

    public void Awake()
    {
        Instance = this;

        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public PlayerAttack GetPlayerAttack() {
        return playerAttack;
    }

    public PlayerMovement GetPlayerMovement() {
        return playerMovement;
    }
}
