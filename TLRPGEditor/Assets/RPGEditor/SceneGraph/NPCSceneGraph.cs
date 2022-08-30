﻿using XNode;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class NPCSceneGraph:SerializedMonoBehaviour
{
	public static NPCSceneGraph Instance;
	public List<NPCNodeGraph> npcs;

    public void Awake()
    {
        Instance = this; 
    }
}