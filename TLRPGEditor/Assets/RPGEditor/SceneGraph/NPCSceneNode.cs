using XNode;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class NPCSceneNode :SceneGraph<NPCNodeGraph>
{
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            graph.MoveNext();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            graph.Start();
        }
    }
}
