using System;
using XNodeEditor;

namespace TLRPGEditor
{
    [CustomNodeGraphEditor(typeof(ProcessNodeGraph))]
    public class ProcessGraphEditor : NodeGraphEditor
    {
        public override string GetNodeMenuName(Type type)
        {
            if (typeof(ProcessNode).IsAssignableFrom(type))
            {
                return base.GetNodeMenuName(type);
            }
            else return null;
        }
    }
}