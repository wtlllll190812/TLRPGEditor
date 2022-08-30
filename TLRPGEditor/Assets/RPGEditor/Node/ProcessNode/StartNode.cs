using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[NodeWidth(100)]
[NodeTint("#2B714C")]
[CreateNodeMenu("流程/开始")]
public class StartNode : ProcessNode
{
	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;

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
}