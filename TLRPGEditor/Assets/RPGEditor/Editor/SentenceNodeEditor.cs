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
        base.OnBodyGUI();

        //if (simpleNode == null) simpleNode = target as SentenceNode;
        //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("npc"));
        //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exit"));
        //EditorGUILayout.RectField(new GUIContent(simpleNode.npcSprite),);
        ////GUILayout.Box(simpleNode.npcSprite);
        serializedObject.ApplyModifiedProperties();
    }
}
