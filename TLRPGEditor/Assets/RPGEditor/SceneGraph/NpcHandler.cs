using XNode;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace TLRPGEditor
{
    public class NpcHandler:SerializedMonoBehaviour
    {
        public NPCNodeGraph npcNode;
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
            npcNode.currentDialague.MoveNext();
            Execute();
        }

        public void GraphStart()
        {
            onDialogueStart?.Invoke();
            npcNode.currentDialague.Start();
            Execute();
        }

        public void GraphEnd()
        {
            onDialogueEnd?.Invoke();
        }

        public void Execute()
        {
            if (npcNode.currentDialague.currentNode is SentenceNode)
            {
                var node = npcNode.currentDialague.currentNode as SentenceNode;
                dialogueText.text = node.GetDialogue();
            }
            else if (npcNode.currentDialague.currentNode is EndNode)
            {
                GraphEnd();
            }
            else if (npcNode.currentDialague.currentNode is SwitchNode)
            {
                dialogueoptions.options.Clear();
                var node = npcNode.currentDialague.currentNode as SwitchNode;
                foreach (var item in node.options)
                {
                    dialogueoptions.options.Add(new TMP_Dropdown.OptionData(item));
                }
                switchNode = node;
            }
        }

        public void SetSwitchNode(int index)
        {
            if (switchNode != null)
                switchNode.result = index;
        }

        public void SetDialogue(string dialogue)
        {
            dialogueText.text = dialogue;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                npcNode.currentDialague.MoveNext();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                npcNode.currentDialague.Start();
            }
        }
    }
}