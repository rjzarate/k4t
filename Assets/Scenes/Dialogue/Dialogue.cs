using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// this is the editor class for formatting the dialogue
// this is displayed in the inspector once you see the "Dialogue System" text

using UnityEditor;
using System.Reflection;

#if UNITY_EDITOR
[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{

    // editting the inspector to make Dialogue flexible and display name and Dialogue in order

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

            // order name and dialogue, dialogue flexible with text area
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

// main class

public class Dialogue : MonoBehaviour
{
    [SerializeField] string[] nameStr;
    [SerializeField] string[] dialogueStr;
    [SerializeField] int count;
    int endCount;
    [SerializeField] GameObject[] DialogueObjs;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    //setup
    // endcount initially set to the length of entire dialogue
    // set the first count
    // 
    void Start()
    {
        endCount = dialogueStr.Length-1;
        displayText(0);
        foreach ( GameObject DialogueObj in DialogueObjs)
        {
            DialogueObj.SetActive(false);
        }
    }

    // set the text to the string based on the current set count
    public void displayText( int orderCount)
    {
        if (dialogueStr.Length > orderCount)
        {
            nameText.text = nameStr[orderCount];
            dialogueText.text = dialogueStr[orderCount];
        }
    }

    // go to the next dialogue
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

    // go to previous dialogue
    public void dialoguePrevious()
    {
        if (count > 0)
        {
            count--;
            displayText(count);
        }
    }

    // turn dialogue on and off
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

    // ***************************************************************
    // ***************************************************************
    // USE THIS FOR CALLING IT
    // call the dialogue, this is the key function
    // that can be called by external sources

    // end point is where ever you want to end the count at
    // this way you can call multiple times, maybe during battle
    // ***************************************************************
    public void callDialogue( int endPoint)
    {
        enableDialogue(true);
        endCount = endPoint;
    }
}