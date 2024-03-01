using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAttack : MonoBehaviour
{
    [SerializeField] private List<string> targets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // method to shoot a raycast and apply effects
    public void ShootRay<T>(Vector2 sourcePosition, Vector2 targetPosition, List<T> hitEffects)
    {
        Debug.Log("Ray Shot");
        int layerMask = LayerMaskByNameList(targets);
        RaycastHit2D hit = Physics2D.Raycast(sourcePosition, targetPosition - sourcePosition, float.PositiveInfinity, layerMask);
        if (!hit)
        {
            return;
        }
        Debug.Log("Object Hit");
        IHittable comp = hit.collider.gameObject.GetComponent<IHittable>();
        if (comp != null)
        {
            comp.TriggerEffects(hitEffects);
        }
    }

    // get layer number
    private int LayerMaskByNameList(List<string> layers)
    {
        int layerMask = 0;
        foreach (string layer in layers)
        {
            int layerNumber = LayerMask.NameToLayer(layer);
            if (layerNumber >= 0)
            {
                layerMask |= 1 << layerNumber;
            }
        }

        return layerMask;
    }
}