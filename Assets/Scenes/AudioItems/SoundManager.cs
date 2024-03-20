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
        BossReceiveHit.Instance.OnBossDamageSoundEvent += Boss_OnBossDamageSoundEvent;
    }

    private void Player_OnTapSoundEvent()
    {
        PlayerMovementAndAttackSubscriber playerMovementAndAttackSubscriber = PlayerMovementAndAttackSubscriber.Instance;
        PlaySound(audioClipReferencesSO.playerFire, playerMovementAndAttackSubscriber.transform.position);
    }

   
    private void Boss_OnBossDamageSoundEvent()
    {
        BossReceiveHit bossRecieveHit = BossReceiveHit.Instance;
        PlaySound(audioClipReferencesSO.metalicBossDamageTaken, bossRecieveHit.transform.position);
    }
    

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
