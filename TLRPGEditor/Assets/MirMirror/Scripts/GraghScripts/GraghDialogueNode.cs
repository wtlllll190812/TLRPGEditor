using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirMirror;
namespace MirMirror
{
    [System.Serializable]
    public class GraghDialogueNode : MyGraghNode
    {


        public Rect m_InRect { get { return new Rect(m_Rect.x, m_Rect.y, 20, 20); } }
        public Rect m_OutRect { get { return new Rect(m_Rect.x + m_Rect.width - 20, m_Rect.y, 20, 20); } }

        public GraghDialogueNode(Vector2 startPos, Vector2 size, NodeID_Type _NodeID_Type, MM_DialogueNodeEditor _Editor) : base(startPos, size, _NodeID_Type,_Editor)
        {
            m_InPointsID = new List<int>();
            m_OutPointsID = new List<int>();
            ConnectionPoint tempPoint = new ConnectionPoint(this, ConnectionPointType.In, 0,_Editor);
            m_InPointsID.Add(tempPoint.m_PointID);
            tempPoint = new ConnectionPoint(this, ConnectionPointType.Out, 0,_Editor);
            m_OutPointsID.Add(tempPoint.m_PointID);



            m_Editor.CurMMDialogue_Gragh.m_GraghDialogueNodes.Add(this);
            m_Editor.CurMMDialogue_Gragh.m_MMDialogue_Data.m_DataDialogueNodes.Add(new DataDialogueNode(this.m_NodeID_Type.ID));

        }
        public GraghDialogueNode(GraghDialogueNode graghDialogueNode, MM_DialogueNodeEditor _Editor) : base(graghDialogueNode,_Editor)
        {

        }
        public NodeID_Type GetDataNode()
        {
            return m_NodeID_Type;
        }
    }
}
