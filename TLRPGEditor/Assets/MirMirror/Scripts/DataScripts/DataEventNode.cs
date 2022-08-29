using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MirMirror
{
    [System.Serializable]
    public class DataEventNode : DataMMDialogueNode
    {
        [SerializeField]
        private string m_EventKey;
        public string EventKey { get { return m_EventKey; } set { m_EventKey = value; } }
        public DataEventNode(int dataID) : base()
        {

            m_DataNodeID = new NodeID_Type(dataID, NODETYPE.EVENT);
            m_NextNodes.Add(new NodeID_Type(-1, NODETYPE.START));
        }
        public DataEventNode(DataEventNode dataEventNode) : base(dataEventNode)
        {

            m_EventKey = dataEventNode.m_EventKey;
        }
    }
}
