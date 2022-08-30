using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[NodeWidth(400)]
[NodeTint("#942424")]//Node颜色
[System.Serializable]
[CreateNodeMenu("Event/AddEvent")]
public class EventNode:ProcessNode
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;
	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;

	[OnValueChanged("Init")]
	public string eventName;
	[OnValueChanged("AddEvent")]
	public UnityEvent nodeEvent;

	protected override void Init() 
	{
		base.Init();
		if (EventQueue.FindEvent(eventName) != null)
			nodeEvent = EventQueue.FindEvent(eventName);
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
		AddEvent();
		MoveNext();
	}

	public void AddEvent()
    {
		EventQueue.AddEvent(eventName, nodeEvent);
	}
}