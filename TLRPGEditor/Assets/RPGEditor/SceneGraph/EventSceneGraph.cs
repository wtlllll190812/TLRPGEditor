using XNode;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;

public class EventSceneGraph : SceneGraph<EventNodeGraph>
{
    public void Start()
    {
        graph.Init();
    }
}
