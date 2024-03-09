using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(Effect))]
public class EffectPropertyDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Grab attributes of class
        SerializedProperty effectType = property.FindPropertyRelative(nameof(effectType));
        SerializedProperty genericFloat = property.FindPropertyRelative(nameof(genericFloat));
        SerializedProperty genericFloat1 = property.FindPropertyRelative(nameof(genericFloat1));
        SerializedProperty genericFloat2 = property.FindPropertyRelative(nameof(genericFloat2));
        SerializedProperty genericUnitInterval = property.FindPropertyRelative(nameof(genericUnitInterval));

        EditorGUI.BeginProperty(position, label, property);
        
        EditorGUILayout.PropertyField(effectType);

        // Show attributes of corresponding effect type
        Effect.EffectType _effectType = (Effect.EffectType)effectType.enumValueIndex;
        switch (_effectType)
        {

            case Effect.EffectType.Null:
                // Do nothing
                break;

            case Effect.EffectType.Damage:
                OnGUIDamage(genericFloat);
                break;

            case Effect.EffectType.Slow:
                OnGUISlow(genericUnitInterval);
                break;

            case Effect.EffectType.Poison:
                OnGUIPoison(genericFloat, genericFloat1, genericFloat2);
                break;

            default:
                Debug.LogError("Unimplemented Editor Display: " + _effectType);
                break;
        }


        EditorGUI.EndProperty();
    }

    private void OnGUIPoison(SerializedProperty genericFloat, SerializedProperty genericFloat1, SerializedProperty genericFloat2)
    {
        EditorGUILayout.PropertyField(genericFloat, new GUIContent("Posion Damage Amount"));
        EditorGUILayout.PropertyField(genericFloat1, new GUIContent("Poison Proc Time"));
        EditorGUILayout.PropertyField(genericFloat2, new GUIContent("Poison Duration"));
    }

    private void OnGUISlow(SerializedProperty genericUnitInterval)
    {
        EditorGUILayout.PropertyField(genericUnitInterval, new GUIContent("Slow Percentage"));
    }

    private void OnGUIDamage(SerializedProperty genericFloat) {
        EditorGUILayout.PropertyField(genericFloat, new GUIContent("Damage Amount"));
    }
}
