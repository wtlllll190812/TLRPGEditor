using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventTriggerNode : ProcessNode 
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;

	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;
	
	public string eventName;

	protected override void Init()
	{
		base.Init();
	}

	public override object GetValue(NodePort port)
	{
		return null;
	}

	public override void MoveNext()
	{
		EventQueue.instance.InVokeEvent(eventName);
		MoveNextNode();
	}

	public override void OnEnter()
	{
		
	}
}