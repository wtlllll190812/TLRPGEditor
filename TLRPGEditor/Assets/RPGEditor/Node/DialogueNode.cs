using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[NodeWidth(400)]
[NodeTint("#3f6fa0")]//Node颜色
public class DialogueNode : RPGNode
{
	public List<string> Dialogues;

	private int currentIndex=0;
	//[Input(backingValue= ShowBackingValue.Never)] public float value;
	//[Output] public float result;

	// Use this for initialization
	protected override void Init()
	{
		base.Init();
	}

	// Return the correct value of an output port when requested
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

	public override void Execute()
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