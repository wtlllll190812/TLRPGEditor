using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[CreateAssetMenu]
public class EventNodeGraph : NodeGraph
{ 
	public Dictionary<string, UnityEvent> events;
	
	public void AddNewEvent(string eventName,UnityEvent _event)
    {
		events[eventName] = _event;
    }
}