using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TLRPGEditor
{
    [System.Serializable]
    public abstract class ProcessNode : Node
    {
        protected ProcessNodeGraph rpgGraph;

        protected override void Init()
        {
            rpgGraph = graph as ProcessNodeGraph;
        }

        public virtual void ResetNode() { }

        protected void MoveNextNode()
        {
            var node = GetOutputPort("exit").Connection?.node as ProcessNode;
            if (node == null) Debug.LogError("NodePort is null");
            rpgGraph.currentNode = node;
            ResetNode();
            node.OnEnter();
        }

        public abstract void MoveNext();

        public abstract void OnEnter();
    }
}