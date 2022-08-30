using XNode;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[NodeWidth(200)]
[NodeTint("#3f6fa0")]
[CreateNodeMenu("数据/获取触发器")]
public class GetTriggerNode : ProcessNode
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;
	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;

	public string triggerName;

	protected override void Init()
	{
		base.Init();
		rpgGraph.SetStartNode(this);
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port)
	{
		return null; // Replace this
	}

	public override void MoveNext()
	{
		if(PlayerPrefs.HasKey(triggerName))
			MoveNextNode();
	}

	public override void OnEnter()
	{
		MoveNext();
	}
}