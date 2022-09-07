using XNode;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace TLRPGEditor
{
	[NodeWidth(200)]
	[NodeTint("#2B714C")]
	[CreateNodeMenu("流程/开始")]
	[NodeTitle("对话开始")]
	public class DialogueStartNode : ProcessNode
	{
		[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
		public bool exit;

		public string diaogueName;

		protected override void Init()
		{
			base.Init();
			rpgGraph.SetStartNode(this);
		}

		public override object GetValue(NodePort port)
		{
			return null;
		}

		public override void MoveNext()
		{
			MoveNextNode();
		}

		public override void OnEnter()
		{
			Debug.Log("RPGNode Start");
			MoveNext();
		}

		[Button("AddDialogue")]
		public void AddDialogue()
		{
			ProcessNodeGraph.ProcessNodes.Add(diaogueName, rpgGraph);
		}
	}
}