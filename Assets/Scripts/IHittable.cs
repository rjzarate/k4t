using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void TriggerEffects<T>(List<T> effects);
}
