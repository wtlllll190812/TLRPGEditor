using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MirMirror
{
    [System.Serializable]
    public class DataStartNode : DataMMDialogueNode
    {
        [SerializeField]
        protected string m_ChrName;
        public string ChrName { get { return m_ChrName; } set { m_ChrName = value; } }
        [SerializeField]
        protected int m_PartID;
        public int PartID { get { return m_PartID; } set { m_PartID = value; } }
        public DataStartNode() : base()
        {

            m_DataNodeID = new NodeID_Type(0, NODETYPE.START);

            m_NextNodes.Add(new NodeID_Type(-1, NODETYPE.START));
        }
        public DataStartNode(DataStartNode dataStartNode) : base(dataStartNode)
        {
            m_ChrName = dataStartNode.m_ChrName;
            m_PartID = dataStartNode.PartID;


        }




    }
}
