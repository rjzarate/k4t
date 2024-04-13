using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script to allow one track to be played first, then another to be the "loop track"
[RequireComponent(typeof(AudioSource))]
public class MusicLooper: MonoBehaviour
{
    [SerializeField] private AudioSource introSource;
    [SerializeField] private AudioSource loopSource;
    //private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        double time = AudioSettings.dspTime;
        introSource.PlayScheduled(0.5 + time);
        loopSource.PlayScheduled(0.5 + time + introSource.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
