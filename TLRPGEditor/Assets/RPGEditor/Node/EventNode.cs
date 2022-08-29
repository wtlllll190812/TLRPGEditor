using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[NodeWidth(400)]
[NodeTint("#942424")]//Node颜色
[System.Serializable]
public class EventNode:Node
{
	public EventNodeGraph eventGraph;
	public string eventName;
	public UnityEvent nodeEvent;

	protected override void Init() 
	{
		base.Init();
		eventGraph = graph as EventNodeGraph;
	}

	public override object GetValue(NodePort port) 
	{
		return null;
	}
}