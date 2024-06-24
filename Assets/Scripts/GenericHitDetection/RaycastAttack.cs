using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAttack : MonoBehaviour
{
    [SerializeField] private List<string> targetLayers;
    

    // method to shoot a raycast and apply effects
    public void ShootRay(Vector2 sourcePosition, Vector2 targetPosition, List<Effect> effects)
    {
        Debug.Log("Ray Shot");
        int layerMask = LayerMaskByNameList(targetLayers);
        RaycastHit2D hit = Physics2D.Raycast(sourcePosition, targetPosition - sourcePosition, float.PositiveInfinity, layerMask);
        if (!hit)
        {
            return;
        }
        Debug.Log("Object Hit");
        IHittable hittable = hit.collider.gameObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittable.TriggerEffects(effects);
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