using System;
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
    private void Start()
    {

        StartCoroutine(PlayMusicRoutine());
        
    }

    private IEnumerator PlayMusicRoutine()
    {
        yield return new WaitUntil(() => introSource.clip.loadState == AudioDataLoadState.Loaded);
        double time = AudioSettings.dspTime;
        // WEBGL IS WEIRD RN MAKING CLIP.LENGTH 0 NO MATTER WHAT I DO
        // introSource.PlayScheduled(0.5 + time);
        // loopSource.PlayScheduled(0.5 + time + introSource.clip.length);
        loopSource.PlayScheduled(0.5 + time );
    }

    // Update is called once per frame
    void Update()
    {
    }
}
