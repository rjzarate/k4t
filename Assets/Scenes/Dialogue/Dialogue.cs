using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEditor;
using System.Reflection;

#if UNITY_EDITOR

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Dialogue dialogue = (Dialogue)target;

        FieldInfo fieldInfo = typeof(Dialogue).GetField("dialogueStr", BindingFlags.NonPublic | BindingFlags.Instance);
        string[] dialogueStr = (string[])fieldInfo.GetValue(dialogue);

        FieldInfo nameFieldInfo = typeof(Dialogue).GetField("nameStr", BindingFlags.NonPublic | BindingFlags.Instance);
        string[] nameStr = (string[])nameFieldInfo.GetValue(dialogue);

        if (dialogueStr != null && nameStr != null)
        {
            EditorGUILayout.LabelField("\n\n");
            EditorGUILayout.LabelField("\n\n");
            EditorGUILayout.LabelField("------------------------------------------------------------------------------------------------");
            EditorGUILayout.LabelField("\t\t\t\t\tDialogue System:");
            EditorGUILayout.LabelField("------------------------------------------------------------------------------------------------");
            EditorGUILayout.LabelField("\n\n");
            for (int i = 0; i < dialogueStr.Length; i++)
            {
                EditorGUILayout.BeginVertical();
                nameStr[i] = EditorGUILayout.TextField("Name", nameStr[i]);
                EditorGUILayout.LabelField("Dialogue:");
                dialogueStr[i] = EditorGUILayout.TextArea(dialogueStr[i]);
                EditorGUILayout.EndVertical();
            }
        }
    }
}
#endif


public class Dialogue : MonoBehaviour
{
    [SerializeField] string[] nameStr;
    [SerializeField] string[] dialogueStr;
    [SerializeField] int count;
    int endCount;
    [SerializeField] GameObject[] DialogueObjs;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    void Start()
    {
        endCount = dialogueStr.Length-1;
        displayText(0);
        foreach ( GameObject DialogueObj in DialogueObjs)
        {
            DialogueObj.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void displayText( int orderCount)
    {
        if (dialogueStr.Length > orderCount)
        {
            nameText.text = nameStr[orderCount];
            dialogueText.text = dialogueStr[orderCount];
        }
    }

    public void dialogueNext()
    {
        count++;
        if (count < dialogueStr.Length && count <= endCount)
        {
            displayText(count);
        }

        else {
            enableDialogue(false);
        }
    }

    public void dialoguePrevious()
    {
        if (count > 0)
        {
            count--;
            displayText(count);
        }
    }

    void enableDialogue( bool isEnabled)
    {
        if (isEnabled)
        {
            foreach (GameObject DialogueObj in DialogueObjs)
            {
                DialogueObj.SetActive(true);
            }
        }

        else {
            foreach (GameObject DialogueObj in DialogueObjs)
            {
                DialogueObj.SetActive(false);
            }
        }
    }

    public void callDialogue( int endPoint)
    {
        enableDialogue(true);
        endCount = endPoint;
    }
}