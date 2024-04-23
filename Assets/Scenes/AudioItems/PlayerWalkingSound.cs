using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingSound : MonoBehaviour
{
    // members to aid with the Player's walking ound
    [SerializeField] private PlayerMovementAndAttackSubscriber playerMovementAndAttackSubscriber;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovementAndAttackSubscriber.OnWalkSoundEvent += PlayerMovementAndAttackSubscriber_OnWalkSoundEvent;
    }

    // function used to determine when to play the player's walking sound
    private void PlayerMovementAndAttackSubscriber_OnWalkSoundEvent(bool playerCanMakeWalkingSound)
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
