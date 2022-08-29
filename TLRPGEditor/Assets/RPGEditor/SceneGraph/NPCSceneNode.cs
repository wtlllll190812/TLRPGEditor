using XNode;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class NPCSceneNode :SceneGraph<NPCNodeGraph>
{
    public TMPro.TMP_Text TMPro;
    public UnityEvent onDialogueStart;
    public UnityEvent onDialogueEnd;

    public void MoveNext()
    {
        graph.MoveNext();
        if (graph.currentNode is DialogueNode)
        {
            var node = graph.currentNode as DialogueNode;
            TMPro.text = node.GetDialogue();
        }
        else if(graph.currentNode is EndNode)
        {
            GraphEnd();
        }
    }

    public void GraphStart()
    {
        onDialogueStart?.Invoke();
        graph.Start();
    }

    public void GraphEnd()
    {
        onDialogueEnd?.Invoke();
    }

    public void SetDialogue(string dialogue)
    {
        TMPro.text = dialogue;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            graph.MoveNext();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            graph.Start();
        }
    }
}
