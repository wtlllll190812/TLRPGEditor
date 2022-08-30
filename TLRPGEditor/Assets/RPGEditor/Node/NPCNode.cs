using XNode;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[NodeWidth(300)]
[NodeTint("#B78352")]
[CreateNodeMenu("Process/NPCNode")]
public class NPCNode : ProcessNode 
{
	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;

	public string npcName;
	[PreviewField]
	public Sprite npcSprite;

	protected override void Init() {
		base.Init();
		rpgGraph.SetStartNode(this);
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

    public override void MoveNext()
    {
    }

    public override void OnEnter()
    {
    }
}