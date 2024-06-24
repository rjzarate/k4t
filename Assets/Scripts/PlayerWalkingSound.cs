using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingSound : MonoBehaviour
{
    private AudioSource audioSource;
	public int sampleDataLength = 1024;

	private float[] clipSampleData;

    private bool isPlayerWalking = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        clipSampleData = new float[sampleDataLength];

        PlayerMovement playerMovement = Player.Instance.GetPlayerMovement();
        playerMovement.OnWalkSoundEvent += PlayerMovement_OnWalkSoundEvent;
    }

	// Update is called once per frame
	void Update () {
        if (!isPlayerWalking && audioSource.isPlaying) {
            float clipLoudness = CalculateClipLoudness();
            if (clipLoudness < 0.01f) {
                audioSource.Pause();
            }
        }
        
	}

    private float CalculateClipLoudness() {
        audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
        float clipLoudness = 0f;
        foreach (var sample in clipSampleData) {
            clipLoudness += Mathf.Abs(sample);
        }
        clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
        return clipLoudness;
		
    }

    // function used to determine when to play the player's walking sound
    private void PlayerMovement_OnWalkSoundEvent(bool playerCanMakeWalkingSound)
    {
        // determines whether the walking sound should be played
        if (playerCanMakeWalkingSound) // true if the player is allowed to move
        {
            audioSource.Play();
            isPlayerWalking = true;
        }
        else
        {
            // audioSource.Pause();
            isPlayerWalking = false;
        }
    }
}
