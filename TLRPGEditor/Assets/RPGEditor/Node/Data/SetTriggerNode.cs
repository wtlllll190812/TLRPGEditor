using XNode;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[NodeWidth(200)]
[NodeTint("#3f6fa0")]
[CreateNodeMenu("数据/设置触发器")]
public class SetTriggerNode : ProcessNode
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
		return null; 
	}

	public override void MoveNext()
	{
		MoveNextNode();
	}

	public override void OnEnter()
	{
		PlayerPrefs.SetString(triggerName, "true");
		MoveNext();
	}
}
