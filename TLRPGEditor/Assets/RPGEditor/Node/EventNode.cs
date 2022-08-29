using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[NodeWidth(400)]
[NodeTint("#942424")]//Node颜色
public class EventNode:Node
{
	public EventNodeGraph eventGraph;
	public string eventName;
	public UnityEvent @event;

	protected override void Init() 
	{
		base.Init();
		eventGraph = graph as EventNodeGraph;
		eventGraph.AddNewEvent(eventName,@event);
	}

	public override object GetValue(NodePort port) 
	{
		return null;
	}
}