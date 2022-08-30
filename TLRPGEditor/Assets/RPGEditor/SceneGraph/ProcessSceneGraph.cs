using XNode;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class ProcessSceneGraph :SceneGraph<ProcessNodeGraph>
{
    public TMP_Text dialogueText;
    public TMP_Dropdown dialogueoptions;

    public UnityEvent onDialogueStart;
    public UnityEvent onDialogueEnd;

    private SwitchNode switchNode;

    public void Awake()
    {
        dialogueoptions.onValueChanged.AddListener(SetSwitchNode);
    }

    public void MoveNext()
    {
        graph.MoveNext();
        Execute();
    }

    public void GraphStart()
    {
        onDialogueStart?.Invoke();
        graph.Start();
        Execute();
    }

    public void GraphEnd()
    {
        onDialogueEnd?.Invoke();
    }

    public void Execute()
    {
        if (graph.currentNode is SentenceNode)
        {
            var node = graph.currentNode as SentenceNode;
            dialogueText.text = node.GetDialogue();
        }
        else if (graph.currentNode is EndNode)
        {
            GraphEnd();
        }
        else if(graph.currentNode is SwitchNode)
        {
            dialogueoptions.options.Clear();
            var node = graph.currentNode as SwitchNode;
            foreach (var item in node.options)
            {
                dialogueoptions.options.Add(new TMP_Dropdown.OptionData(item));
            }
            switchNode = node;
        }
    }

    public void SetSwitchNode(int index)
    {
        if(switchNode!=null)
            switchNode.result = index;
    }

    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
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
