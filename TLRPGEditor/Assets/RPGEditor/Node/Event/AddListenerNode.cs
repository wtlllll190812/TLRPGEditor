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
[CreateNodeMenu("事件/为事件添加行为")]
public class AddListenerNode : ProcessNode
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;
	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;

	[OnValueChanged("Init")]
	[ValueDropdown("eventList")]
	public string eventName;
	public UnityEvent newEvent;
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
	
	private UnityEvent nodeEvent;

	protected override void Init()
	{
		base.Init();
		if (rpgGraph.eventQueue.FindEvent(eventName) != null)
			nodeEvent = rpgGraph.eventQueue.FindEvent(eventName);
	}

	public override object GetValue(NodePort port)
	{
		return null;
	}

	public override void MoveNext()
	{
		MoveNextNode();
	}

	public void AddNewListener()
    {
		newEvent.Invoke();
	}

	public override void OnEnter()
	{
		nodeEvent?.AddListener(AddNewListener);
		MoveNext();
	}
}