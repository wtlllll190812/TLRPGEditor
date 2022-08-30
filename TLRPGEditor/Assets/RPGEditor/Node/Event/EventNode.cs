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
[CreateNodeMenu("事件/添加事件")]
public class EventNode:ProcessNode
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;
	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;

	public string eventName;
	public UnityEvent nodeEvent;

	protected override void Init() 
	{
		base.Init();
		if (eventName != null)
			AddEvent();
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

	[Button("AddEvent")]
	public void AddEvent()
    {
		rpgGraph.eventQueue.AddEvent(eventName, nodeEvent);
	}
}