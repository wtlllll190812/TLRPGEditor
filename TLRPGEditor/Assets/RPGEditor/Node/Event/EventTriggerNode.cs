using XNode;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[NodeWidth(200)]
[NodeTint("#942424")]//Node颜色
[CreateNodeMenu("事件/触发事件")]
[System.Serializable]
public class EventTriggerNode : ProcessNode 
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;

	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;
	
	[ValueDropdown("eventList")]
	public string eventName;
	public List<string> eventList 
	{
		get 
		{
			List<string> res = new List<string>();
			foreach (var item in rpgGraph.eventQueue.events.Keys)
            {
				res.Add(item);
            }
			return res;
		} 
	}

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
		rpgGraph.eventQueue.InVokeEvent(eventName);
		MoveNextNode();
	}

	public override void OnEnter()
	{
		MoveNext();
	}
}