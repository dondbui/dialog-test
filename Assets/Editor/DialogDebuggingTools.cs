using core.dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogDebugComponent))]
public class DialogDebuggingTools : Editor
{
    private string fileName = "filename.json";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Episode JSON files must exist in Assets/Resources/JSON");
        fileName = EditorGUILayout.TextArea(fileName);

        //GUILayout.TextField(fileName, 15, "filename.json");

        // This button draws the debug lines for occupied tiles
        if (GUILayout.Button("Load Conversation File and Start"))
        {
            Debug.Log("Textfield: " + fileName);
            DialogController dc = DialogController.GetInstance();

            string jsonFile = fileName;
            if (fileName.Contains(".json"))
            {
                jsonFile = fileName.Replace(".json", "");
            }

            dc.LoadDialogJSON("JSON/" + jsonFile);
            dc.StartConversation();
        }
        
    }
}
