using XNode;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
[System.Serializable]
public class NPCNodeGraph : ProcessNodeGraph
{
	public static List<NPCNodeGraph> npcNodes=new List<NPCNodeGraph>();
	[NonSerialized]public NPCNode npcNode;

    public NPCNodeGraph()
    {
        npcNodes.Add(this);
    }
    public void SetNPCNode(NPCNode node)
	{
		npcNode = node;
	}
}