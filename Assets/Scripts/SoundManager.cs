using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipReferencesSO audioClipReferencesSO;

    private Player player;
    private Boss boss;
    private SpaghettiWhip spaghettiWhip;


    // Start is called before the first frame update
    void Start()
    {
        // Player sounds
        player = Player.Instance;
        PlayerAttack playerAttack = player.GetPlayerAttack();
        playerAttack.OnTapSoundEvent += Player_OnTapSoundEvent;

        // Boss sounds
        boss = Boss.Instance;
        Health bossHealth = boss.GetBossHealth();
        bossHealth.TakeDamageEvent += Boss_TakeDamageSoundEvent;

        List<BossAction> bossActions = boss.GetBossActions();
        spaghettiWhip = bossActions.Find(b => b.GetComponent<SpaghettiWhip>() != null).GetComponent<SpaghettiWhip>();
        spaghettiWhip.OnBossAttackWhipSoundEvent += BossAttack_OnBossAttackWhipSoundEvent;
    }

    private void Player_OnTapSoundEvent()
    {
        PlaySound(audioClipReferencesSO.playerFire, player.transform.position);
    }

    private void Boss_TakeDamageSoundEvent(float newHealth)
    {
        PlaySound(audioClipReferencesSO.metalicBossDamageTaken, boss.transform.position);
    }

    private void BossAttack_OnBossAttackWhipSoundEvent()
    {
        PlaySound(audioClipReferencesSO.bossAttackWhip, spaghettiWhip.transform.position);
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