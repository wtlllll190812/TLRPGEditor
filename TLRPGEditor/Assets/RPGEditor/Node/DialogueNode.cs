using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[NodeWidth(400)]
[NodeTint("#3f6fa0")]//Node颜色
public class DialogueNode : RPGNode
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;
	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;


	public List<string> Dialogues;

	private int currentIndex=0;
	
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
		if(currentIndex == Dialogues.Count-1)
        {
			MoveNextNode();
        }
		currentIndex++;
	}

	public override void OnEnter()
	{
		currentIndex = 0;
	}

	public string GetDialogue()
    {
		return Dialogues[currentIndex];
    }

    public override void ResetNode()
    {
		currentIndex = 0;
    }
}