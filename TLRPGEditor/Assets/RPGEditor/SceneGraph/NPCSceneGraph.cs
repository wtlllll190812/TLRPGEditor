using XNode;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class NPCSceneGraph :SceneGraph<NPCNodeGraph>
{
    public TMP_Text TMPro;
    public TMP_Dropdown options;
    public UnityEvent onDialogueStart;
    public UnityEvent onDialogueEnd;

    private SwitchNode switchNode;


    public void Awake()
    {
        options.onValueChanged.AddListener(SetSwitchNode);
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
        if (graph.currentNode is DialogueNode)
        {
            var node = graph.currentNode as DialogueNode;
            TMPro.text = node.GetDialogue();
        }
        else if (graph.currentNode is EndNode)
        {
            GraphEnd();
        }
        else if(graph.currentNode is SwitchNode)
        {
            options.options.Clear();
            var node = graph.currentNode as SwitchNode;
            foreach (var item in node.options)
            {
                options.options.Add(new TMP_Dropdown.OptionData(item));
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
