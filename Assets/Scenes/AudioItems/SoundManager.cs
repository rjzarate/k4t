using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipReferencesSO audioClipReferencesSO;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementAndAttackSubscriber.Instance.OnTapSoundEvent += Player_OnTapSoundEvent;
        BossReceiveHit.Instance.OnBossDamageSoundEvent += BossDamageTaken_OnBossDamageSoundEvent;
        BossAttackWhip.Instance.OnBossAttackWhipSoundEvent += BossAttack_OnBossAttackWhipSoundEvent;
    }


    private void Player_OnTapSoundEvent()
    {
        PlayerMovementAndAttackSubscriber playerMovementAndAttackSubscriber = PlayerMovementAndAttackSubscriber.Instance;
        PlaySound(audioClipReferencesSO.playerFire, playerMovementAndAttackSubscriber.transform.position);
    }

    private void BossDamageTaken_OnBossDamageSoundEvent()
    {
        BossReceiveHit bossRecieveHit = BossReceiveHit.Instance;
        PlaySound(audioClipReferencesSO.metalicBossDamageTaken, bossRecieveHit.transform.position);
    }

    private void BossAttack_OnBossAttackWhipSoundEvent()
    {
        BossAttackWhip bossAttackWhip = BossAttackWhip.Instance;
        PlaySound(audioClipReferencesSO.bossAttackWhip, bossAttackWhip.transform.position);
    }


    // functions to play the sounds

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}


// used to implement further sounds

/*// plays the sound for the boss taking damage
 * OnBossDamageSoundEvent();
 * // delegated event for the boss's damage receive sound sounds
public delegate void BossDamageSoundEventHandler();
public event BossDamageSoundEventHandler OnBossDamageSoundEvent;
 */