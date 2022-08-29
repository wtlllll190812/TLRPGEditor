using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[NodeWidth(500)]
[NodeTint("#B78352")]
public class SwitchNode : RPGNode
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;

	[Output(dynamicPortList = true)] public string[] options;
	public int? result=null;

	protected override void Init() 
	{
		base.Init();
	}

	public override object GetValue(NodePort port) 
	{
		return null; // Replace this
	}

    public override void MoveNext()
    {
		if(result!=null)
        {
			var node = GetOutputPort("options "+(int)result).Connection?.node as RPGNode;
			if (node == null) Debug.LogError("NodePort is null");
			rpgGraph.currentNode = node;
			ResetNode();
		}
    }

    public override void OnEnter()
    {
		result = null;
	}
}