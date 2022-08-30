using XNode;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateNodeMenu("Event/EventTrigger")]
public class EventTriggerNode : ProcessNode 
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;

	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;
	
	[ValueDropdown("PlayerLevel")]
	public string eventName;
	public static List<string> PlayerLevel { get {
			List<string> res = new List<string>();
			foreach (var item in EventQueue.events.Keys)
            {
				res.Add(item);
            }
			return res;
		} }

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
		EventQueue.InVokeEvent(eventName);
		MoveNextNode();
	}

	public override void OnEnter()
	{
		MoveNext();
	}
}