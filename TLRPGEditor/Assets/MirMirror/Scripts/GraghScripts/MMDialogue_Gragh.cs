using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirMirror;
namespace MirMirror
{

    [System.Serializable]
    public class MMDialogue_Gragh : ScriptableObject
    {
        [SerializeField]
        public MMDialogue_Data m_MMDialogue_Data;

        [SerializeField]
        public GraghStartNode m_GraghStartNode;

        [SerializeField]
        public List<GraghDialogueNode> m_GraghDialogueNodes;
        [SerializeField]
        public List<GraghChoiceNode> m_GraghChoiceNodes;
        [SerializeField]
        public List<GraghEventNode> m_GraghEventNodes;
        [SerializeField]
        public List<ConnectionLine> m_Lines;
        [SerializeField]
        public List<ConnectionPoint> m_Points;

        public int GetNodeUniqueID(NODETYPE nODETYPE)
        {
            switch (nODETYPE)

            {
                case NODETYPE.START:
                    return 0;

                case NODETYPE.DIALOGUE:
                    if (m_GraghDialogueNodes.Count == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return m_GraghDialogueNodes[m_GraghDialogueNodes.Count - 1].DataNode.ID + 1;

                    }

                case NODETYPE.CHOICE:
                    if (m_GraghChoiceNodes.Count == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return m_GraghChoiceNodes[m_GraghChoiceNodes.Count - 1].DataNode.ID + 1;
                    }


                case NODETYPE.EVENT:
                    if (m_GraghEventNodes.Count == 0)
                    {
                        return 0;
                    }
                    else
                    {

                        return m_GraghEventNodes[m_GraghEventNodes.Count - 1].DataNode.ID + 1;
                    }

                default:
                    return -1;
            }
        }
        public int GetPointUniqueID()
        {
            if (m_Points.Count == 0)
            {
                return 0;

            }
            else
            {
                return m_Points[m_Points.Count - 1].m_PointID + 1;
            }
        }
        public ConnectionPoint GetPoint(int _PointID)
        {
            for (int i = 0; i < m_Points.Count; i++)
            {
                if (m_Points[i].m_PointID == _PointID)
                {
                    return m_Points[i];
                }
            }
            return null;
        }
        public GraghDialogueNode GetGraghDialogueNode(int dataID)
        {
            for (int i = 0; i < m_GraghDialogueNodes.Count; i++)
            {
                if (m_GraghDialogueNodes[i].DataNode.ID == dataID)
                {
                    return m_GraghDialogueNodes[i];
                }
            }
            return null;
        }
        public GraghChoiceNode GetGraghChoiceNode(int dataID)
        {
            for (int i = 0; i < m_GraghChoiceNodes.Count; i++)
            {
                if (m_GraghChoiceNodes[i].DataNode.ID == dataID)
                {
                    return m_GraghChoiceNodes[i];
                }
            }
            return null;
        }
        public GraghEventNode GetGraghEventNode(int dataID)
        {
            for (int i = 0; i < m_GraghEventNodes.Count; i++)
            {
                if (m_GraghEventNodes[i].DataNode.ID == dataID)
                {
                    return m_GraghEventNodes[i];
                }
            }
            return null;
        }
        public void RemoveGraghDialogueNode(int dataID)
        {
            for (int i = 0; i < m_GraghDialogueNodes.Count; i++)
            {
                if (m_GraghDialogueNodes[i].DataNode.ID == dataID)
                {
                    m_GraghDialogueNodes.RemoveAt(i);
                    break;
                }
            }
        }
        public void RemoveGraghChoieceNode(int dataID)
        {
            for (int i = 0; i < m_GraghChoiceNodes.Count; i++)
            {
                if (m_GraghChoiceNodes[i].DataNode.ID == dataID)
                {
                    m_GraghChoiceNodes.RemoveAt(i);
                    break;
                }
            }
        }
        public void RemoveGraghEventNode(int dataID)
        {

            for (int i = 0; i < m_GraghEventNodes.Count; i++)
            {
                if (m_GraghEventNodes[i].DataNode.ID == dataID)
                {
                    m_GraghEventNodes.RemoveAt(i);
                    break;
                }
            }
        }
        public void RemovePoint(int pointID)
        {
            for (int i = 0; i < m_Points.Count; i++)
            {
                if (m_Points[i].m_PointID == pointID)
                {
                    m_Points.RemoveAt(i);
                    break;
                }
            }
        }
        public void ResetGraghData()
        {
            m_GraghStartNode = null;
            m_GraghDialogueNodes.Clear();
            m_GraghChoiceNodes.Clear();
            m_GraghEventNodes.Clear();
            m_Lines.Clear();
            m_Points.Clear();

        }
        public void ResetDataData()
        {
            m_MMDialogue_Data.m_StartNode = null;
            m_MMDialogue_Data.m_DataChoieceNodes.Clear();
            m_MMDialogue_Data.m_DataDialogueNodes.Clear();
            m_MMDialogue_Data.m_DataEventNodes.Clear();

        }

    }
}
