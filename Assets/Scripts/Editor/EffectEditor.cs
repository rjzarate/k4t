using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// [CustomEditor(typeof(Effect))]
// public class EffectEditor : Editor
// {

//     private SerializedProperty effectType;
//     private SerializedProperty genericFloat;
//     private SerializedProperty genericUnitInterval;

//     private void OnEnable() 
//     {
//         effectType = serializedObject.FindProperty(nameof(effectType));
//         genericFloat = serializedObject.FindProperty(nameof(genericFloat));
//         genericUnitInterval = serializedObject.FindProperty(nameof(genericUnitInterval));
//     }

//     public override void OnInspectorGUI()
//     {
//         // Grab script instance
//         Effect effect = (Effect) target;
        
//         // Updates all Monobehavior scripts to display inspector
//         serializedObject.Update();

//         // Always display effect type
//         EditorGUILayout.PropertyField(effectType);
        
//         // Show attributes of corresponding effect type
//         switch (effect.GetEffectType()) {
            
//             case Effect.EffectType.Null:
//                 // Do nothing
//                 break;
            
//             case Effect.EffectType.Damage:
//                 OnInspectorGUIDamage(effect);
//                 break;
            
//             case Effect.EffectType.Slow:
//                 OnInspectorGUISlow(effect);
//                 break;
            
//             default:
//                 Debug.LogError("Unimplemented Editor Display: " + effect.GetEffectType());
//                 break;
//         }
        

//         // Allows changes to be made in the inspector
//         serializedObject.ApplyModifiedProperties();
//     }

//     private void OnInspectorGUISlow(Effect effect)
//     {
//         EditorGUILayout.PropertyField(genericUnitInterval, new GUIContent("Slow Percentage"));
//     }

//     private void OnInspectorGUIDamage(Effect effect) {
//         EditorGUILayout.PropertyField(genericFloat, new GUIContent("Damage Amount"));
//     }
// }
