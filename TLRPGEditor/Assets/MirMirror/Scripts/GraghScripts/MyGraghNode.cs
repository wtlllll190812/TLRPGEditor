
using System.Collections.Generic;
using UnityEngine;
namespace MirMirror
{
    [System.Serializable]
    public abstract class MyGraghNode
    {

        public Rect m_Rect;
        public List<int> m_InPointsID = null;
        public List<int> m_OutPointsID = null;
        public Rect m_TitleRect { get { return new Rect(m_Rect.x, m_Rect.y, m_Rect.width, 20); } }
        [SerializeField]
        protected NodeID_Type m_NodeID_Type;
        public NodeID_Type DataNode { get { return m_NodeID_Type; } set { m_NodeID_Type = value; } }
        public NODETYPE NodeType { get { return m_NodeID_Type.m_NODETYPE; } }

        private bool isDragging;
        protected MM_DialogueNodeEditor m_Editor;

        public MyGraghNode(Vector2 startPos, Vector2 size, NodeID_Type _NodeID_Type, MM_DialogueNodeEditor _Editor)
        {
            m_Editor = _Editor;
            m_Rect = new Rect(startPos, size);
            m_NodeID_Type = _NodeID_Type;

        }
        public MyGraghNode(MyGraghNode myGraghNode, MM_DialogueNodeEditor _Editor)
        {
            m_Editor = _Editor;
            m_Rect = new Rect(myGraghNode.m_Rect);
            m_InPointsID = new List<int>();
            m_OutPointsID = new List<int>();

            for (int i = 0; i < myGraghNode.m_InPointsID.Count; i++)
            {
                m_InPointsID.Add(myGraghNode.m_InPointsID[i]);
            }
            for (int i = 0; i < myGraghNode.m_OutPointsID.Count; i++)
            {
                m_OutPointsID.Add(myGraghNode.m_OutPointsID[i]);
            }
            m_NodeID_Type = new NodeID_Type(myGraghNode.m_NodeID_Type);
            isDragging = false;
        }
        public void ProcessDrag(Vector2 dis)
        {
            m_Rect.position += dis;
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)

            {
                case EventType.MouseDown:
                    if (e.button == 0 && m_TitleRect.Contains(e.mousePosition))
                    {
                        isDragging = true;
                    }

                    break;
                case EventType.MouseUp:
                    isDragging = false;
                    break;
                case EventType.MouseDrag:
                    if (isDragging == true)
                    {
                        ProcessDrag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;

                default:
                    return false;

            }
            return
                false;
        }

    }

}