using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TLRPGEditor
{
	[CreateAssetMenu]
	public class ProcessNodeGraph : NodeGraph
	{
		public static EventQueue eventQueue = new EventQueue();
		public static Dictionary<string, ProcessNodeGraph> ProcessNodes = new Dictionary<string, ProcessNodeGraph>();
        private ProcessNode startNode;
		public ProcessNode currentNode;

		public void SetStartNode(ProcessNode node)
		{
			startNode = node;
		}

		public void Start()
		{
			eventQueue = new EventQueue();
			startNode.OnEnter();
			Debug.Log("start");
		}

		public void MoveNext()
		{
			currentNode.MoveNext();
		}
	}
}