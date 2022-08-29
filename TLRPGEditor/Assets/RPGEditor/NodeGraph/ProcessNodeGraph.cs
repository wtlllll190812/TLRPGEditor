using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class ProcessNodeGraph : NodeGraph
{
	private StartNode startNode;
	public ProcessNode currentNode;

	public void SetStartNode(StartNode node)
	{
		startNode = node;
	}

	public void Start()
	{
		startNode.OnEnter();
	}

	public void MoveNext()
	{
		currentNode.MoveNext();
	}
}