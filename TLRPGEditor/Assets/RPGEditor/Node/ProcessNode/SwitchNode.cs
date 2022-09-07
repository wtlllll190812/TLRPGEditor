using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TLRPGEditor
{
	[NodeWidth(500)]
	[NodeTint("#B78352")]
	[CreateNodeMenu("流程/分支")]
	public class SwitchNode : ProcessNode
	{
		[Input(backingValue = ShowBackingValue.Never)]
		public bool enter;

		[Output(dynamicPortList = true)] public string[] options;
		public int? result = null;

		protected override void Init()
		{
			base.Init();
		}

		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}

		public override void MoveNext()
		{
			if (result != null)
			{
				var node = GetOutputPort("options " + (int)result).Connection?.node as ProcessNode;
				if (node == null) Debug.LogError("NodePort is null");
				rpgGraph.currentNode = node;
				node.OnEnter();
				ResetNode();
			}
		}

		public override void OnEnter()
		{
			result = null;
		}
	}
}