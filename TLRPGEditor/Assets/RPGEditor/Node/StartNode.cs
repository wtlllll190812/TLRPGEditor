using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartNode : RPGNode
{
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

	public override void Execute()
	{
		Debug.Log("RPGNode Start");
		MoveNext();
	}
}