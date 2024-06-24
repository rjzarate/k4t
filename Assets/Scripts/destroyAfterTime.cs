using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // effects before destrying self and after time is up
    [SerializeField] GameObject DestroyEffectPrefab;
    //The time in seconds the gameObject has before it gets deleted from the scene, starting from when it was loaded into it
    public float TotalLifeSeconds;

    [SerializeField] private ParticleSystem[] particlesKept;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf(TotalLifeSeconds));
    }

    // instantiate Destroy effect if it exists
    // then destroy the object
    IEnumerator DestroySelf(float deathTime)
    {
        yield return new WaitForSeconds(deathTime);

        // Keep particles on self distruct. Detaches particle system and them destroys for it's given particle duration
        if (particlesKept != null) {
            foreach (ParticleSystem particleSystem in particlesKept) {
                particleSystem.transform.parent = null;
                particleSystem.Stop();
                particleSystem.AddComponent<DestroyAfterTime>().TotalLifeSeconds = particleSystem.main.duration + 1f;
            }
        }
        
        // Effects on self distruct
        if (DestroyEffectPrefab)
        {
            Instantiate(DestroyEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}