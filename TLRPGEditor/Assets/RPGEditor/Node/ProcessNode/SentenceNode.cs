using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[NodeWidth(400)]
[NodeTint("#3f6fa0")]//Node颜色
[CreateNodeMenu("流程/语句")]
public class SentenceNode : ProcessNode
{
	[Input(backingValue = ShowBackingValue.Never)]
	public bool enter;
	[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
	public bool exit;

	[ValueDropdown("NpcList")]
	[OnValueChanged("OnNpcChange")]
	public NPCNodeGraph npc;
	[PreviewField]
	public Texture npcSprite;

	[MultiLineProperty]
	public List<string> Dialogues;
	
	private List<NPCNodeGraph> NpcList
    { 
		get { return NPCNodeGraph.npcNodes; }
	}
	private int currentIndex=0;
	
	public void OnNpcChange()
    {
		npcSprite = npc.npcNode.npcTex;
	}

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