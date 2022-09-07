using XNode;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace TLRPGEditor
{
    [NodeWidth(200)]
    [NodeTint("#B78352")]
    //[CreateNodeMenu("流程/分支")]
    public class DiologueNode:ProcessNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool enter;
        [Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)]
        public bool exit;

        [OnValueChanged("Init")]
        [ValueDropdown("diologueList")]
        public string diaogueName;

        public List<string> diologueList
        {
            get
            {
                List<string> res = new List<string>();
                foreach (var item in NPCNodeGraph.dialogues.Keys)
                {
                    res.Add(item);
                }
                return res;
            }
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
