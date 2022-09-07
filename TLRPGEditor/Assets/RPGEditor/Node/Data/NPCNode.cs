using XNode;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace TLRPGEditor
{
	[NodeWidth(300)]
	[NodeTint("#3f6fa0")]
	[CreateNodeMenu("NPC/NPC配置")]
	public class NPCNode : ProcessNode
	{
		[HideInInspector] public NPCNodeGraph npcGraph;

		[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
		public bool exit;

		public string npcName;
		[PreviewField]
		public Texture npcTex;

		protected override void Init()
		{
			base.Init();
			npcGraph = graph as NPCNodeGraph;
			npcGraph.SetNPCNode(this);
		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}

		public override void MoveNext()
		{
			MoveNextNode();
		}

		public override void OnEnter()
		{
			MoveNext();
		}
	}
}