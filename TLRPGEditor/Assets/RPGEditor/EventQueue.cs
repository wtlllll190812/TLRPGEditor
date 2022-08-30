using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class EventQueue : SerializedMonoBehaviour
{
    public static Dictionary<string, UnityEvent> events = new Dictionary<string, UnityEvent>();

    public void Awake()
    {
        events.Clear();
    }

    public static void AddEvent(string eventName,UnityEvent @event)
    {
        events[eventName] = @event;
    }

    public static void InVokeEvent(string eventName)
    {
        events[eventName]?.Invoke();
    }

    public static UnityEvent FindEvent(string eventName)
    {
        if(events.ContainsKey(eventName))
            return events[eventName];
        return null;
    }
}
