using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public abstract class RPGNodeGraph : NodeGraph 
{
	private StartNode startNode;
	public RPGNode currentNode;

	public void SetStartNode(StartNode node)
    {
		startNode = node;
	}

	public void Start()
    {
		startNode.Execute();
	}

	public void MoveNext()
    {
		currentNode.MoveNext();
	}
}