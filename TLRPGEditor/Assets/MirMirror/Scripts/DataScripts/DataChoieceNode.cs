using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MirMirror
{
    [System.Serializable]
    public class DataChoieceNode : DataMMDialogueNode
    {
        [SerializeField]
        private List<string> m_Choices = null;
        public DataChoieceNode(int dataID) : base()
        {

            m_DataNodeID = new NodeID_Type(dataID, NODETYPE.CHOICE);
            if (m_Choices == null)
            {
                m_Choices = new List<string>();
            }
            m_Choices.Add("A");
            m_NextNodes.Add(new NodeID_Type(-1, NODETYPE.DIALOGUE));
            m_Choices.Add("B");
            m_NextNodes.Add(new NodeID_Type(-1, NODETYPE.DIALOGUE));
        }
        public DataChoieceNode(DataChoieceNode dataChoieceNode) : base(dataChoieceNode)
        {


            m_Choices = new List<string>();
            for (int i = 0; i < dataChoieceNode.m_Choices.Count; i++)
            {
               
                m_Choices.Add(dataChoieceNode.m_Choices[i]);
            }
        }

        public ref List<string> Choices()
        {
            return ref m_Choices;
        }
    }
}
