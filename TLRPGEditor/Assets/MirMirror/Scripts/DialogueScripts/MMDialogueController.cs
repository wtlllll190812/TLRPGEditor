using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

namespace MirMirror
{


    public class MMDialogueController : MonoBehaviour
    {

        #region
        [SerializeField]
        private MMDialogue_Data m_MMDialogue_Data = null;
        public MMDialogue_Data MMDialogue_Data { get { return m_MMDialogue_Data; } set { m_MMDialogue_Data = value; } }
       
        private bool m_NextBtn;

        private int m_ChoiceID = -1;

        private int m_WordsID = 0;


     
        private DataMMDialogueNode m_CurrentNode = null;
        #endregion


        #region
  
        private AdvancedText m_AdvancedText;
  
        private Widget m_WDialogueCharBGL;

        private Widget m_WDialogueCharBGR;

        private Widget m_WDialogueBG;
    
        private Widget m_WDialogueNextBtn;
     
        private Widget m_WDialogue;

        private AudioClip m_AudioClip;
        [SerializeField]
        private AudioSource m_AudioSource;

        private GameObject m_SelectItem;

        private GameObject m_SelectFrame;

        private Transform m_SelectGroup;
        [SerializeField]
        private List<MMEvent> m_MMEvents;

        #endregion
        private void Awake()
        {
            if (m_WDialogueCharBGL == null)
            {
                m_WDialogueCharBGL = this.transform.GetChild(1).GetComponent<Widget>();
            }
            if (m_WDialogueCharBGR == null)
            {
                m_WDialogueCharBGR = this.transform.GetChild(2).GetComponent<Widget>();
            }
            if (m_WDialogueBG == null)
            {
                m_WDialogueBG = this.transform.GetChild(0).GetComponent<Widget>();
            }
            if (m_WDialogueNextBtn == null)
            {
                m_WDialogueNextBtn = this.transform.GetChild(3).GetComponent<Widget>();
            }
            if (m_WDialogue == null)
            {
                m_WDialogue = this.transform.GetChild(0).GetChild(0).GetComponent<Widget>();
            }
            if (m_AdvancedText == null)
            {
                m_AdvancedText = this.transform.GetChild(0).GetChild(0).GetComponent<AdvancedText>();
            }
            ;
            if (m_WDialogueNextBtn.transform.GetComponent<MyMouseEvent>() != null)
                m_WDialogueNextBtn.transform.GetComponent<MyMouseEvent>().m_MyOnClickLeft.AddListener(() => ClickNextBtn());
            if (m_AudioSource == null)
            {
                m_AudioSource = this.GetComponent<AudioSource>();
            }
            if (m_SelectFrame==null)
            {
                m_SelectFrame = Resources.Load<GameObject>("SelectFrame");
            }
            if (m_SelectItem == null)
            {
                m_SelectItem = Resources.Load<GameObject>("SelectItem");
            }
            if (m_SelectGroup==null)
            {
                m_SelectGroup= this.transform.GetChild(4).transform;
            }
        }

        [ContextMenu("===ResetEvents===")]
        public void ResetEventList()
        {
            if (m_MMDialogue_Data == null)
            {
                Debug.LogWarning("MMDialogue_Data is null!");
                return;
            }
            m_MMEvents = new List<MMEvent>();
            for (int i = 0; i < m_MMDialogue_Data.m_DataEventNodes.Count; i++)
            {
                m_MMEvents.Add(new MMEvent(m_MMDialogue_Data.m_DataEventNodes[i].EventKey));
            }
        }
        [ContextMenu("===StartDialogue===")]
        public void StartDialogue()
        {
            if (m_MMDialogue_Data == null)
            {
                Debug.LogWarning("MMDialogue_Data is null!");
                return;
            }
            StartCoroutine(ParseMM_DialogueData());
            this.GetComponent<Widget>().Fade(1f,0.1f,null);

        }

        private IEnumerator ParseMM_DialogueData()
        {
            m_ChoiceID = -1;

            m_CurrentNode = m_MMDialogue_Data.GetFirstNode();


            while (true)
            {
                switch (m_CurrentNode.DataNodeID.m_NODETYPE)
                {
                    case NODETYPE.START:
                        m_CurrentNode = m_MMDialogue_Data.GetFirstNode();
                        break;
                    case NODETYPE.DIALOGUE:
                        for (int i = 0; i < (m_CurrentNode as DataDialogueNode).Words.Count; i++)
                        {

                            m_WordsID = i;
                            ContinueDialogue(m_CurrentNode as DataDialogueNode);
    
                            while (m_NextBtn == false)
                            {
                                yield return null;
                            }
                            m_NextBtn = false;
                            yield return null;
                        }


                        m_CurrentNode = m_MMDialogue_Data.GetNextDialogueNode(m_CurrentNode);
                        break;
                    case NODETYPE.CHOICE:
                        m_ChoiceID = -1;
                        ShowChoiceDialogue(m_CurrentNode as DataChoieceNode);
                        while (m_ChoiceID < 0)
                        {
                            yield return null;
                        }

               
                        m_CurrentNode = m_MMDialogue_Data.GetNextDialogueNode(m_CurrentNode, m_ChoiceID);
                        m_ChoiceID = -1;
                        break;
                    case NODETYPE.EVENT:
                        TriggerEvent((m_CurrentNode as DataEventNode).EventKey);

                        m_CurrentNode = m_MMDialogue_Data.GetNextDialogueNode(m_CurrentNode);
                        break;
                    default:
                        break;
                }


                if (m_CurrentNode == null)
                {
                    EndDialogue();
                    break;
                }
                yield return null;
            }
        }
        private void TriggerEvent(string _EventKey)
        {
            for (int i = 0; i < m_MMEvents.Count; i++)
            {
                if (_EventKey == m_MMEvents[i].m_EventKey)
                {
                    m_MMEvents[i].m_Event.Invoke();
                }
            }
        }
        private void ContinueDialogue(DataDialogueNode _DataNode)
        {
            if (_DataNode.SpeakPos==SpeakPos.L)
            {
                ShowDialogueL(_DataNode);
            }
            else
            {
                ShowDialogueR(_DataNode);
            }
           
        }

        public void ShowDialogueL(DataDialogueNode _DataNode)
        {

            m_WDialogueCharBGL.transform.GetChild(0).GetComponent<Image>().sprite = _DataNode.ChrImgs[m_WordsID];
            m_AudioClip = _DataNode.Voices[m_WordsID];
            

            m_WDialogueCharBGL.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _DataNode.ChrName;
            m_WDialogueCharBGL.Fade(1, 0.2f, null);
            m_WDialogueBG.Fade(1, 0f, StartShowContent);
            m_WDialogueCharBGR.Fade(0, 0.2f, null);
        }

        public void ShowDialogueR(DataDialogueNode _DataNode)
        {
            m_WDialogueCharBGR.transform.GetChild(0).GetComponent<Image>().sprite = _DataNode.ChrImgs[m_WordsID];
            m_AudioClip = _DataNode.Voices[m_WordsID];
           
            m_WDialogueCharBGR.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _DataNode.ChrName;
            m_WDialogueCharBGR.Fade(1, 0.2f, null);
            m_WDialogueBG.Fade(1, 0f, StartShowContent);
            m_WDialogueCharBGL.Fade(0, 0.2f, null);
        }
        private List<GameObject> m_ChoieceGameObjects;
        private GameObject m_ChoiceCursor;
        public void ShowChoiceDialogue(DataChoieceNode _DataNode)
        {

            int choiceCount = _DataNode.Choices().Count;
            m_ChoieceGameObjects = new List<GameObject>();
            for (int i = 0; i < choiceCount; i++)
            {
                m_ChoieceGameObjects.Add(Instantiate(m_SelectItem, m_SelectGroup));
            }
            for (int i = 0; i < choiceCount; i++)
            {

                int temp = i;
                m_ChoieceGameObjects[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _DataNode.Choices()[temp];
                m_ChoieceGameObjects[i].GetComponent<Widget>().Fade(1, 0.1f, null);
                m_ChoieceGameObjects[i].GetComponent<MyMouseEvent>().m_MyOnEnter.AddListener(() => OnMyMouseEnter(temp));
            }

            m_ChoiceCursor = Instantiate(m_SelectFrame, m_ChoieceGameObjects[0].transform);
            m_ChoiceCursor.GetComponent<MyMouseEvent>().m_MyOnClickLeft.AddListener(() => OnMyMouseClick());
            m_ChoiceCursor.GetComponent<Widget>().Fade(1, 0.1f, null);
        }

        private int m_CurSelectID = 0;
        private void OnMyMouseEnter(int _ChoiceID)
        {
 
            m_CurSelectID = _ChoiceID;
            m_ChoiceCursor.transform.SetParent(m_ChoieceGameObjects[m_CurSelectID].transform);
            m_ChoiceCursor.transform.localPosition = Vector3.zero;
        }
        private void OnMyMouseClick()
        {
 
            m_ChoiceID = m_CurSelectID;
            m_CurSelectID = 0;
            m_ChoiceCursor.GetComponent<Animator>().SetTrigger("Click");
            m_ChoiceCursor.GetComponent<Widget>().Fade(0, 0.4f, DeleteSelectItem);
        }
        private void DeleteSelectItem()
        {
            if (m_ChoieceGameObjects != null)
            {
                for (int i = 0; i < m_ChoieceGameObjects.Count; i++)
                {

                    Destroy(m_ChoieceGameObjects[i]);
                }
                m_ChoieceGameObjects.Clear();
            }

            if (m_ChoiceCursor != null)
            {
                Destroy(m_ChoiceCursor);
                m_ChoiceCursor = null;
            }
        }
        private void EndDialogue()
        {
  
            this.GetComponent<Widget>().Fade(0, 0.2f, null);
        }
        private void StartShowContent()
        {
           
            if (m_AudioClip != null)
            {
                m_AudioSource.clip = m_AudioClip;

                m_AudioSource.Play();
            }
            else
            {
                m_AudioSource.Stop();
            }


            m_WDialogue.Fade(1, 0f, StartTyp);
            m_WDialogueNextBtn.Fade(1, 0.2f, null);

        }
        private void StartTyp()
        {
            if (m_WDialogueBG.transform.GetChild(0).childCount > 0)
            {

                Destroy(m_WDialogueBG.transform.GetChild(0).GetChild(0).gameObject);
            }
            m_AdvancedText.ShowTextByTyping((m_CurrentNode as DataDialogueNode).Words[m_WordsID]);
        }
        private void ClickNextBtn()
        {
            m_WDialogueNextBtn.transform.GetComponent<Animator>().SetTrigger("Click");
            if (m_AdvancedText.IsTypingOver)
            {

                m_AdvancedText.IsTypingOver = false;
                m_NextBtn = true;
            }
            else
            {
                m_AdvancedText.OverleapDialogue();
            }

        }

    }
    [CustomEditor(typeof(MMDialogueController))]
    public class ObjectBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MMDialogueController myScript = (MMDialogueController)target;
            if (GUILayout.Button("Start"))
            {
                myScript.StartDialogue();
            }
            if (GUILayout.Button("ResetEvents"))
            {
                myScript.ResetEventList();
            }

        }

    }
}
namespace MirMirror
{
    [System.Serializable]
    public struct MMEvent
    {
        public string m_EventKey;
        public UnityEvent m_Event;
        public MMEvent(string _EventKey)
        {
            m_EventKey = _EventKey;
            m_Event = null;
        }
    }
}
