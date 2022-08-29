using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class RPGNode : Node 
{
    protected RPGNodeGraph rpgGraph;
    [Input(backingValue = ShowBackingValue.Never)] public bool enter;
    [Output(backingValue = ShowBackingValue.Never)] public bool exit;

    protected override void Init()
    {
        rpgGraph = graph as RPGNodeGraph;
    }

    public virtual void ResetNode() { }

    protected void MoveNextNode()
    {
        var node = GetOutputPort("exit").Connection?.node as RPGNode;
        if (node == null) Debug.LogError("NodePort is null");
        rpgGraph.currentNode = node;
        ResetNode();
        node.OnEnter();
    }

    public abstract void MoveNext();

    public abstract void OnEnter();
}