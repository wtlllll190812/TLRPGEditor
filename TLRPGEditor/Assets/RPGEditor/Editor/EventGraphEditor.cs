using System;
using XNodeEditor;

[CustomNodeGraphEditor(typeof(EventNodeGraph))]
public class EventGraphEditor : NodeGraphEditor
{
    public override string GetNodeMenuName(Type type)
    {
        if (typeof(EventNode).IsAssignableFrom(type))
        {
            return base.GetNodeMenuName(type);
        }
        else return null;
    }
}