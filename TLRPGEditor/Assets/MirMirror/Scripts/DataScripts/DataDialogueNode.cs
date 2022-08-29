using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MirMirror
{
    [System.Serializable]
    public class DataDialogueNode : DataMMDialogueNode
    {
        [SerializeField]
        private string m_ChrName;
        public string ChrName { get { return m_ChrName; } set { m_ChrName = value; } }

        [SerializeField]
        private List<Sprite> m_ChrImgs;
        public List<Sprite> ChrImgs { get { return m_ChrImgs; } set { ChrImgs = value; } }
        [SerializeField]
        private List<string> m_Words = null;
        public List<string> Words { get { return m_Words; } set { Words = value; } }
        [SerializeField]
        private List<AudioClip> m_Voices = null;
        public List<AudioClip> Voices { get { return m_Voices; } set { Voices = value; } }
        [SerializeField]
        private SpeakPos m_SpeakPos= SpeakPos.L;
        public SpeakPos SpeakPos { get { return m_SpeakPos; } set { m_SpeakPos = value; } }
        public DataDialogueNode(int dataID) : base()
        {
         
            m_DataNodeID = new NodeID_Type(dataID, NODETYPE.DIALOGUE);
            if (m_ChrImgs == null)
            {
                m_ChrImgs = new List<Sprite>();
            }
            if (m_Words == null)
            {
                m_Words = new List<string>();
            }
            if (m_Voices==null)
            {
                m_Voices = new List<AudioClip>();
            }
            m_ChrName = "MirMirror";
            m_ChrImgs.Add(null);
            m_Words.Add("Content...");
            m_Voices.Add(null);
            m_NextNodes.Add(new NodeID_Type(-1, NODETYPE.START));
            m_SpeakPos = SpeakPos.L;
        }
        public DataDialogueNode(DataDialogueNode dataDialogueNode) : base(dataDialogueNode)
        {

            m_ChrName = dataDialogueNode.m_ChrName;
            m_ChrImgs = new List<Sprite>();
            for (int i = 0; i < dataDialogueNode.m_ChrImgs.Count; i++)
            {
                m_ChrImgs.Add(dataDialogueNode.m_ChrImgs[i]);
            }
            m_Words = new List<string>();
            for (int i = 0; i < dataDialogueNode.m_Words.Count; i++)
            {
         
                m_Words.Add(dataDialogueNode.m_Words[i]);
            }
            m_Voices = new List<AudioClip>();

            for (int i = 0; i < dataDialogueNode.m_Voices.Count; i++)
            {
                m_Voices.Add(dataDialogueNode.m_Voices[i]);
            }
            m_SpeakPos = dataDialogueNode.SpeakPos;
        }



        
    }
}
