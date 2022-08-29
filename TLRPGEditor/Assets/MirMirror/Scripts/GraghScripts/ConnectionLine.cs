using MirMirror;
using UnityEngine;
using UnityEditor;
namespace MirMirror
{
    [System.Serializable]
    public class ConnectionLine
    {
        [SerializeField]
        protected int m_StartPointID;
        [SerializeField]
        protected int m_EndPointID;
        public ConnectionPoint m_StartPoint { get { return m_Editor.CurMMDialogue_Gragh.GetPoint(m_StartPointID); } }
        public ConnectionPoint m_EndPoint { get { return m_Editor.CurMMDialogue_Gragh.GetPoint(m_EndPointID); } }
        private MM_DialogueNodeEditor m_Editor;
        

        public ConnectionLine(int _StartPointID, int _EndPointID, MM_DialogueNodeEditor _Editor)
        {
            m_StartPointID = _StartPointID;
            m_EndPointID = _EndPointID;
            m_Editor = _Editor;


            switch (m_StartPoint.Owner.NodeType)

            {
                case NODETYPE.START:
                    m_Editor.CurMMDialogue_Gragh.m_MMDialogue_Data.m_StartNode.NextNodes()[0] = new NodeID_Type(m_EndPoint.Owner.DataNode);
                    break;
                case NODETYPE.DIALOGUE:
                    m_Editor.CurMMDialogue_Gragh.m_MMDialogue_Data.m_DataDialogueNodes[m_StartPoint.Owner.DataNode.ID].NextNodes()[0] = new NodeID_Type(m_EndPoint.Owner.DataNode);
                    break;
                case NODETYPE.CHOICE:
                    m_Editor.CurMMDialogue_Gragh.m_MMDialogue_Data.m_DataChoieceNodes[m_StartPoint.Owner.DataNode.ID].NextNodes()[m_StartPoint.m_NodePointNo] = new NodeID_Type(m_EndPoint.Owner.DataNode);
                    break;
                case NODETYPE.EVENT:
                    m_Editor.CurMMDialogue_Gragh.m_MMDialogue_Data.m_DataEventNodes[m_StartPoint.Owner.DataNode.ID].NextNodes()[0] = new NodeID_Type(m_EndPoint.Owner.DataNode);
                    break;
                default:
                    break;
            }

        }
        public ConnectionLine(ConnectionLine connectionLine, MM_DialogueNodeEditor _Editor)
        {
            m_Editor = _Editor;
            m_StartPointID = connectionLine.m_StartPointID;
            m_EndPointID = connectionLine.m_EndPointID;
            

        }

        public void Draw()
        {

            Handles.DrawBezier(m_StartPoint.m_Rect.center, m_EndPoint.m_Rect.center,
                m_StartPoint.m_Rect.center - Vector2.left * 50f,
                m_EndPoint.m_Rect.center + Vector2.left * 50f,
                Color.red,
                null,
                4.0f);

            if (m_Editor.IsDeleteLineMode)
            {
                Vector2 buttonSize = new Vector2(20, 20);
                Vector2 LineCenter = (m_StartPoint.m_Rect.center + m_EndPoint.m_Rect.center) / 2;
                if (GUI.Button(new Rect(LineCenter - buttonSize / 2, buttonSize), "¡Á"))
                {


                    NodeID_Type targetNode = this.m_StartPoint.Owner.DataNode;
                    switch (targetNode.m_NODETYPE)

                    {
                        case NODETYPE.START:
                            m_Editor.CurMMDialogue_Gragh.m_MMDialogue_Data.m_StartNode.NextNodes()[0] = new NodeID_Type(-1, NODETYPE.START);
                            break;
                        case NODETYPE.DIALOGUE:
                            m_Editor.CurMMDialogue_Gragh.m_MMDialogue_Data.m_DataDialogueNodes[targetNode.ID].NextNodes()[0] = new NodeID_Type(-1, NODETYPE.START);
                            break;
                        case NODETYPE.CHOICE:
                            m_Editor.CurMMDialogue_Gragh.m_MMDialogue_Data.m_DataChoieceNodes[targetNode.ID].NextNodes()[m_StartPoint.m_NodePointNo] = new NodeID_Type(-1, NODETYPE.START);
                            break;
                        case NODETYPE.EVENT:
                            break;
                        default:
                            break;
                    }

                    m_Editor.CurMMDialogue_Gragh.m_Lines.Remove(this);
                }

            }

        }
    }
}


