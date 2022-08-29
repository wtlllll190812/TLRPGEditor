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
        foreach (var item in GetOutputPort("exit").GetConnections())
        {
			var node= item.node as RPGNode;
			node.OnEnter();
		} 
	}

	public override void OnEnter()
	{
		MoveNext();
	}
}