using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class EventNodeGraph : NodeGraph
{ 
	public void Init()
    {
        foreach (EventNode item in nodes)
        {
            EventQueue.instance.AddEvent(item.eventName,item.nodeEvent);
        }
    }
}