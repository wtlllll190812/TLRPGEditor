using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[NodeWidth(100)]
[NodeTint("#9A4747")]
public class EndNode : RPGNode
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;

	protected override void Init() {
		base.Init();
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

	public override void MoveNext()
	{
		//MoveNextNode();
	}

	public override void OnEnter()
	{
		Debug.Log("RPGNode End");
	}
}