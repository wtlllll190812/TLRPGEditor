using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[NodeWidth(200)]
[NodeTint("#3f6fa0")]//Node颜色
public class SwitchNode : RPGNode
{
	[Input] public string t1;
	[Output(dynamicPortList = true)] public string[] options;

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
        throw new System.NotImplementedException();
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }
}