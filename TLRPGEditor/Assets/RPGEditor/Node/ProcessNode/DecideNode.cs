using XNode;
using UnityEngine;
using System.Collections.Generic;

namespace TLRPGEditor
{
    [NodeWidth(500)]
    [NodeTint("#B78352")]
    [CreateNodeMenu("流程/判断")]
    public class DecideNode : ProcessNode
    {
        [Input(backingValue = ShowBackingValue.Never, dynamicPortList = true)]
        public List<bool> enter;
        [Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
        public bool exit;

        protected override void Init()
        {
            base.Init();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }

        public override void MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }
    }
}