using XNode;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace TLRPGEditor
{
	[NodeWidth(200)]
	[NodeTint("#2B714C")]
	[CreateNodeMenu("流程/开始")]
	[NodeTitle("对话开始")]
	public class DialogueStartNode : ProcessNode
	{
		[Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
		public bool exit;

		public string diaogueName;
		[HorizontalGroup("NpcList", 0.8f, LabelWidth = 40)]
		[ValueDropdown("NpcList")]
		[OnValueChanged("OnNpcChange")]
		public NPCNodeGraph npc;

		[HorizontalGroup("NpcList", 0.5f)]
		[PreviewField(80)]
		[HideLabel]
		[ReadOnly]
		public Texture npcSprite;

		private List<NPCNodeGraph> NpcList
		{
			get { return NPCNodeGraph.npcNodes; }
		}
		public void OnNpcChange()
		{
			npcSprite = npc.npcNode.npcTex;
		}
		protected override void Init()
		{
			base.Init();
			rpgGraph.SetStartNode(this);
		}

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
			Debug.Log("RPGNode Start");
			MoveNext();
		}

		[Button("AddDialogue")]
		public void AddDialogue()
		{
			NPCNodeGraph.dialogues.Add(diaogueName, rpgGraph as DialogueNodeGraph);
		}
	}
}