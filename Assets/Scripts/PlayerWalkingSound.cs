using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement playerMovement = Player.Instance.GetPlayerMovement();
        playerMovement.OnWalkSoundEvent += PlayerMovement_OnWalkSoundEvent;
    }

    // function used to determine when to play the player's walking sound
    private void PlayerMovement_OnWalkSoundEvent(bool playerCanMakeWalkingSound)
    {
        // determines whether the walking sound should be played
        if (playerCanMakeWalkingSound) // true if the player is allowed to move
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
