using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // effects before destrying self and after time is up
    [SerializeField] GameObject DestroyEffectPrefab;
    //The time in seconds the gameObject has before it gets deleted from the scene, starting from when it was loaded into it
    public float TotalLifeSeconds;

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

        if (DestroyEffectPrefab)
        {
            Instantiate(DestroyEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}