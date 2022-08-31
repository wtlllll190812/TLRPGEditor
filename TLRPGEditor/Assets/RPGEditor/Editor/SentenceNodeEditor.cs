using XNodeEditor;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomNodeEditor(typeof(SentenceNode))]
public class SentenceNodeEditor : NodeEditor
{
    private SentenceNode simpleNode;

    public override void OnBodyGUI()
    {
        serializedObject.Update();
        //base.OnBodyGUI();

        if (simpleNode == null) simpleNode = target as SentenceNode;
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("npcSprite"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("enter"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exit"));
        EditorGUILayout.LabelField(new GUIContent(simpleNode.npcSprite));
        //GUILayout.Box(simpleNode.npcSprite);
        serializedObject.ApplyModifiedProperties();
    }
}
