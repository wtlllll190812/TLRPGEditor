using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirMirror;

namespace MirMirror
{
    [System.Serializable]
    public class ConnectionPoint
    {
        public MM_DialogueNodeEditor m_Editor;
        public Rect m_Rect;

        #region
        public int m_PointID;
        public int m_NodePointNo;
        public ConnectionPointType m_ConnectionPointType;
        public NodeID_Type m_Owner;
        #endregion
        public MyGraghNode Owner
        {
            get
            {
                switch (m_Owner.m_NODETYPE)
                {
                    case NODETYPE.START:
                        return m_Editor.CurMMDialogue_Gragh.m_GraghStartNode;

                    case NODETYPE.DIALOGUE:
                        return m_Editor.CurMMDialogue_Gragh.GetGraghDialogueNode(m_Owner.ID);
                    case NODETYPE.CHOICE:
                        return m_Editor.CurMMDialogue_Gragh.GetGraghChoiceNode(m_Owner.ID);
                    case NODETYPE.EVENT:
                        return m_Editor.CurMMDialogue_Gragh.GetGraghEventNode(m_Owner.ID);

                    default:
                        return null;

                }

            }

        }
        public ConnectionPoint(MyGraghNode owner, ConnectionPointType connectionPointType, int PointNo, MM_DialogueNodeEditor _Editor)
        {
            m_Editor = _Editor;
            m_PointID = m_Editor.CurMMDialogue_Gragh.GetPointUniqueID();
            m_Owner = new NodeID_Type(owner.DataNode.ID, owner.DataNode.m_NODETYPE);
            m_ConnectionPointType = connectionPointType;
            m_NodePointNo = PointNo;
            m_Rect = new Rect(0, 0, 10, 10);
            m_Editor.CurMMDialogue_Gragh.m_Points.Add(this);

        }
        public ConnectionPoint(ConnectionPoint connectionPoint, MM_DialogueNodeEditor _Editor)
        {
            m_Editor = _Editor;
            m_PointID = connectionPoint.m_PointID;
            m_Rect = new Rect(connectionPoint.m_Rect);
            m_Owner = new NodeID_Type(connectionPoint.m_Owner);
            m_NodePointNo = connectionPoint.m_NodePointNo;
            m_ConnectionPointType = connectionPoint.m_ConnectionPointType;

        }


    }
    public enum ConnectionPointType
    {
        In,
        Out
    }
}