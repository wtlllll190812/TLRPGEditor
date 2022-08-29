using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MirMirror
{
    [System.Serializable]
    public abstract class DataMMDialogueNode
    {
  
        [SerializeField]
        protected NodeID_Type m_DataNodeID;
        public NodeID_Type DataNodeID { get { return m_DataNodeID; } set { m_DataNodeID = value; } }
        [SerializeField]
        protected List<NodeID_Type> m_NextNodes;
        public DataMMDialogueNode()
        {
            if (m_NextNodes == null)
            {
                m_NextNodes = new List<NodeID_Type>();
            }
        }
        public DataMMDialogueNode(DataMMDialogueNode dataMMNode)
        {
            m_DataNodeID = new NodeID_Type(dataMMNode.m_DataNodeID);
            m_NextNodes = new List<NodeID_Type>();
            for (int i = 0; i < dataMMNode.m_NextNodes.Count; i++)
            {

                m_NextNodes.Add(new NodeID_Type(dataMMNode.m_NextNodes[i]));
            }
        }
        public ref List<NodeID_Type> NextNodes()
        {
            return ref m_NextNodes;
        }
    }
    [System.Serializable]
    public struct NodeID_Type
    {
        public int ID;
        public NODETYPE m_NODETYPE;

        public NodeID_Type(int _ID, NODETYPE _NODETYPE)
        {
            ID = _ID;
            m_NODETYPE = _NODETYPE;
        }
        public NodeID_Type(NodeID_Type nodeID_Type)
        {
            ID = nodeID_Type.ID;
            m_NODETYPE = nodeID_Type.m_NODETYPE;
        }
    }
}

