using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndNode : RPGNode
{
	// Use this for initialization
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