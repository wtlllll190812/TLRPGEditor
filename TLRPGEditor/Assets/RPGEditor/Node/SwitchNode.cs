using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[NodeWidth(500)]
[NodeTint("#3f6fa0")]//Node颜色
public class SwitchNode : RPGNode
{
	//public new bool exit;
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
        //throw new System.NotImplementedException();
    }

    public override void OnEnter()
    {
		result = null;
		//throw new System.NotImplementedException();
	}
}