using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[NodeWidth(400)]
[NodeTint("#3f6fa0")]//Node颜色
public class DialogueNode : Node
{
	public string[] x;
	public UnityEvent end;
	[Input] public float value;
	[Output] public float result;

	// Use this for initialization
	protected override void Init()
	{
		base.Init();
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port)
	{
		return null; // Replace this
	}
}