using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[NodeWidth(400)]
[NodeTint("#942424")]//Node颜色
public class EventNode : RPGNode
{
	public UnityEvent @event;

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

	public override void MoveNext()
	{
		throw new System.NotImplementedException();
	}

	public override void OnEnter()
	{
		throw new System.NotImplementedException();
	}
}