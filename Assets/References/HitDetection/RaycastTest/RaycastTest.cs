using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] private Vector2 target;
    [SerializeField] private bool shoot;
    private RaycastAttack raycastAttack;
    // Start is called before the first frame update
    private void Awake()
    {
        raycastAttack = GetComponent<RaycastAttack>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            List<Effect> effects = new List<Effect>();
            // effects.Add("test effect");

            raycastAttack.ShootRay(gameObject.transform.position, target, effects);
        }
        shoot = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gameObject.transform.position, target - (Vector2)gameObject.transform.position);
    }
}