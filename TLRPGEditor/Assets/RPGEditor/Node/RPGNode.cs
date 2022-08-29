using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class RPGNode : Node 
{
    protected RPGNodeGraph rpgGraph;
    [Input(backingValue = ShowBackingValue.Never)] public bool enter;
    [Output(backingValue = ShowBackingValue.Never)] public bool exit;

    protected override void Init()
    {
        rpgGraph = graph as RPGNodeGraph;
    }

    public abstract void MoveNext();

    public abstract void OnEnter();
}