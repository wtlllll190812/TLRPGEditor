using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class EventQueue : SerializedMonoBehaviour
{
    public static EventQueue instance;
    public Dictionary<string, UnityEvent> events = new Dictionary<string, UnityEvent>();

    public void Awake()
    {
        instance = this;    
    }

    public void AddEvent(string eventName,UnityEvent @event)
    {
        events[eventName] = @event;
    }

    public void InVokeEvent(string eventName)
    {
        events[eventName]?.Invoke();
    }
}
