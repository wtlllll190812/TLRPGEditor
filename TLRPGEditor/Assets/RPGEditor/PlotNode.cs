using XNode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[NodeWidth(200)]
[NodeTint("#3fa063")]//Node颜色
public class PlotNode : Node {

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}