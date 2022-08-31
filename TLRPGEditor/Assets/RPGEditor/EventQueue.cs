using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventQueue
{
    public Dictionary<string, UnityEvent> events = new Dictionary<string, UnityEvent>();

    public void AddEvent(string eventName, UnityEvent @event)
    {
        if (eventName == null)
        {
            return;
        }
        events[eventName] = @event;
    }

    public void RemoveEvent(string eventName)
    {
        if (eventName == null)
            return;
        if (events.ContainsKey(eventName))
            events.Remove(eventName);
    }

    public void InVokeEvent(string eventName)
    {
        if (eventName == null)
            return ;
        events[eventName]?.Invoke();
    }

    public UnityEvent FindEvent(string eventName)
    {
        if(eventName==null)
            return null;
        if(events.ContainsKey(eventName))
            return events[eventName];
        return null;
    }
}
