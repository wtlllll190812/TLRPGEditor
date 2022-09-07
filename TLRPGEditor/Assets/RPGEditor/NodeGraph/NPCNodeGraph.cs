using XNode;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TLRPGEditor
{
    [CreateAssetMenu]
    [System.Serializable]
    public class NPCNodeGraph : ProcessNodeGraph
    {
        public static List<NPCNodeGraph> npcNodes = new List<NPCNodeGraph>();
		public static Dictionary<string, DialogueNodeGraph> dialogues = new Dictionary<string, DialogueNodeGraph>();
        [NonSerialized] public NPCNode npcNode;
        public DialogueNodeGraph currentDialague;
        public NPCNodeGraph()
        {
            npcNodes.Add(this);
        }
        public void SetNPCNode(NPCNode node)
        {
            npcNode = node;
        }
    }
}