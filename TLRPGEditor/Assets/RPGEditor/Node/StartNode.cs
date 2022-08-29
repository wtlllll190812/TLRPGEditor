using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[NodeWidth(100)]
[NodeTint("#2B714C")]
public class StartNode : RPGNode
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