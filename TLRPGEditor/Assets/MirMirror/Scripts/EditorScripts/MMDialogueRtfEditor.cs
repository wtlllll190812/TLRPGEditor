using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using MirMirror;
using System.Text;
using System.Collections.Generic;


namespace MirMirror
{
    public class MMDialogueRtfEditor : EditorWindow
    {
        private bool m_HasInitialize = false;
        private int m_MaxRow;
        private int m_MaxCol;


        private DataDialogueNode m_Node;
        private int m_DialogueID;
        private string m_SourceContent;
        private string m_ShowContent;


        private GUIStyle m_IntStyle;
        private GUIStyle m_NormalStyle;

        private List<GUIStyle> m_CharacterStyleList;
        private int m_LcharacterIndex;
        private int m_RcharacterIndex;
        private bool m_IsSetCharacterTypeMode = false;

        private List<Rect> m_ColorRectList;
        private List<Rect> m_CharacterRects;

        private Texture2D m_CharacterBG_N;
        private Texture2D m_CharacterBG_C;
        private Texture2D m_CharacterBG_R;
  
        #region
        private Color m_ColorField;
        #endregion

        #region   
        private float[] m_IntervalList;
        private int[] m_FontSizes;
        private bool[] m_Blods;
        private bool[] m_Italics;
        private string[] m_Colors;
        private List<RubyData> m_RubyList;
        private string m_CurRuby;
        private int m_FontSize;
        #endregion

        #region
        private Dictionary<int, float> m_IntervalListP;
        private List<FontColor> m_ColorsP;
        private List<FontSize> m_FontSizesP;
        private List<FontTypeData> m_BlodsP;
        private List<FontTypeData> m_ItalicsP;
        #endregion

        public void OpenWindow()
        {
            MMDialogueRtfEditor editor = GetWindow<MMDialogueRtfEditor>(false, "MMDialogueSpeedEditor");
        }
        public void SetNode(DataDialogueNode _Node, int _DialogueID)
        {
            m_HasInitialize = false;
            m_Node = _Node;
            m_SourceContent = m_Node.Words[_DialogueID];
            m_DialogueID = _DialogueID;
            m_FontSize = 20;

            ParasContent(m_SourceContent);
            UpadateDynamicStyle();
            m_HasInitialize = true;
        }
        private void OnGUI()
        {
            if (m_HasInitialize == false)
            {
                return;
            }
            CalculateColRow();
            DrawHeader();
            DrawToolbar();
            DrawContent();
            DrawRuby();
            ProcessEvent(Event.current);
            UpdateCharacterBG();
            UpdateFontStyle();
            if (GUI.changed)
            {
                Repaint();
            }
        }
        private void OnEnable()
        {

            InitializeGUIStyle();
        }
        private void ParasContent(string _Text)
        {
            PreprocessText(_Text);

            m_CharacterRects = new List<Rect>();


        }
        private void PreprocessText(string _Text)
        {
            m_RubyList = new List<RubyData>();
            m_IntervalListP = new Dictionary<int, float>();
            m_BlodsP = new List<FontTypeData>();
            m_ItalicsP = new List<FontTypeData>();
            m_FontSizesP = new List<FontSize>();
            m_ColorsP = new List<FontColor>();

            string pattern = "<.*?>";
            string text = _Text;
            Match match = Regex.Match(text, pattern);
            while (match.Success)
            {

                string label = match.Value.Substring(1, match.Length - 2);
                if (float.TryParse(label, out float result))
                {
            
                    m_IntervalListP.Add(match.Index - 1, result);
                }
                else if (Regex.IsMatch(label, "^r=.*"))
                {
                    m_RubyList.Add(new RubyData(match.Index, label.Substring(2)));
                }
                else if (label == "/r")
                {
                    if (m_RubyList.Count > 0)
                    {
                        m_RubyList[m_RubyList.Count - 1].EndIndex = match.Index - 1;
                    }
                }
                else if (label == "b")
                {
                    m_BlodsP.Add(new FontTypeData(match.Index));
                }
                else if (label == "/b")
                {
                    if (m_BlodsP.Count > 0)
                    {
                        m_BlodsP[m_BlodsP.Count - 1].EndIndex = match.Index - 1;
                    }
                }
                else if (label == "i")
                {
                    m_ItalicsP.Add(new FontTypeData(match.Index));
                }
                else if (label == "/i")
                {
                    if (m_ItalicsP.Count > 0)
                    {
                        m_ItalicsP[m_ItalicsP.Count - 1].EndIndex = match.Index - 1;
                    }
                }
                else if (Regex.IsMatch(label, "^size=.*"))
                {
                    if (int.TryParse(label.Substring(5), out int reIn))
                    {
                        m_FontSizesP.Add(new FontSize(match.Index, reIn));
                    }
                    else
                    {
                        m_FontSizesP.Add(new FontSize(match.Index, 0));
                    }

                }
                else if (label == "/size")
                {
                    if (m_FontSizesP.Count > 0)
                    {
                        m_FontSizesP[m_FontSizesP.Count - 1].EndIndex = match.Index - 1;
                    }
                }
                else if (Regex.IsMatch(label, "^color=.*"))
                {

                    m_ColorsP.Add(new FontColor(match.Index, label.Substring(6)));


                }
                else if (label == "/color")
                {
                    if (m_ColorsP.Count > 0)
                    {
                        m_ColorsP[m_ColorsP.Count - 1].EndIndex = match.Index - 1;
                    }
                }
                text = text.Remove(match.Index, match.Length);
                if (text.Length == 0)
                {
                    break;
                }
                match = Regex.Match(text, pattern);
            }
            text = _Text;
            pattern = @"<(\d+)(\.\d+)?>|(</r>|(<r=.*?>))|(</b>|<b>)|(</i>|<i>)|(</size>|(<size=.*?>))|(</color>|(<color=.*?>))";
            text = Regex.Replace(text, pattern, "");
            m_ShowContent = text;


            #region
            m_IntervalList = new float[m_ShowContent.Length];
            m_FontSizes = new int[m_ShowContent.Length];
            m_Blods = new bool[m_ShowContent.Length];
            m_Italics = new bool[m_ShowContent.Length];
            m_Colors = new string[m_ShowContent.Length];
            for (int i = 0; i < m_IntervalList.Length; i++)
            {
                m_IntervalList[i] = -1;
                foreach (var item in m_IntervalListP)
                {
                    if (item.Key == i)
                    {
                        m_IntervalList[i] = item.Value;
                    }
                }

            }
            for (int i = 0; i < m_FontSizes.Length; i++)
            {
                m_FontSizes[i] = 20;
                for (int j = 0; j < m_FontSizesP.Count; j++)
                {
                    if (i >= m_FontSizesP[j].StartIndex && i <= m_FontSizesP[j].EndIndex)
                    {
                        m_FontSizes[i] = m_FontSizesP[j].Size;
                    }
                }

            }
            for (int i = 0; i < m_Blods.Length; i++)
            {
                m_Blods[i] = false;
                for (int j = 0; j < m_BlodsP.Count; j++)
                {
                    if (i >= m_BlodsP[j].StartIndex && i <= m_BlodsP[j].EndIndex)
                    {
                        m_Blods[i] = true;
                    }
                }
            }
            for (int i = 0; i < m_Italics.Length; i++)
            {
                m_Italics[i] = false;
                for (int j = 0; j < m_ItalicsP.Count; j++)
                {
                    if (i >= m_ItalicsP[j].StartIndex && i <= m_ItalicsP[j].EndIndex)
                    {
                        m_Italics[i] = true;
                    }
                }
            }
            for (int i = 0; i < m_Colors.Length; i++)
            {
                m_Colors[i] = "#FFFFFF";
                for (int j = 0; j < m_ColorsP.Count; j++)
                {
                    if (i >= m_ColorsP[j].StartIndex && i <= m_ColorsP[j].EndIndex)
                    {
                        m_Colors[i] = m_ColorsP[j].Color;
                    }
                }
                
            }
            

            #endregion 
        }
        private void Reset()
        {
            if (m_Colors != null)
            {

                for (int i = 0; i < m_Colors.Length; i++)
                {
                    m_Colors[i] = "#FFFFFF";
                }
            }
            if (m_IntervalList != null)
            {

                for (int i = 0; i < m_IntervalList.Length; i++)
                {
                    m_IntervalList[i] = -1;
                }
            }
            if (m_FontSizes != null)
            {

                for (int i = 0; i < m_FontSizes.Length; i++)
                {
                    m_FontSizes[i] = 20;
                }
            }
            if (m_Blods != null)
            {

                for (int i = 0; i < m_Blods.Length; i++)
                {
                    m_Blods[i] = false;
                }
            }
            if (m_Italics != null)
            {

                for (int i = 0; i < m_Italics.Length; i++)
                {
                    m_Italics[i] = false;
                }
            }
            if (m_Colors != null)
            {

                for (int i = 0; i < m_Colors.Length; i++)
                {
                    m_Colors[i] = "#FFFFFF";
                }
            }
            if (m_RubyList!=null)
            {

                m_RubyList.Clear();
            }
        }
        private void CalculateColRow()
        {
            m_MaxCol = (int)((position.width) / 50);
            m_MaxRow = (int)(position.height / 100);
            int characterIndex = 0;
            m_CharacterRects.Clear();


            for (int i = 0; i < m_MaxRow; i++)
            {
                for (int j = 0; j < m_MaxCol; j++)
                {
                    characterIndex = i * m_MaxCol + j;
                    if (characterIndex >= m_ShowContent.Length)
                    {
                        return;
                    }
                    m_CharacterRects.Add(new Rect(10 + 50 * j, 95 + 120 * i, 40, 40));

                }
            }

        }
        private void DrawToolbar()
        {
            Rect toolbarRect = new Rect(0, position.height - 120, position.width, 120);
            EditorGUI.DrawRect(toolbarRect, Color.black);
            m_ColorField = EditorGUI.ColorField(new Rect(toolbarRect.x + 5, toolbarRect.y + 5, 40, 40), m_ColorField);

            if (GUI.Button(new Rect(toolbarRect.x + 50, toolbarRect.y + 5, 40, 40), "<"))
            {
                SetColor(-1);
            }

            if (GUI.Button(new Rect(toolbarRect.x + 5, position.height - 50, 40, 20), "B"))
            {
                SetBlod(true);
            }
            if (GUI.Button(new Rect(toolbarRect.x + 5, position.height - 25, 40, 20), "B_"))
            {
                SetBlod(false);
            }
            if (GUI.Button(new Rect(toolbarRect.x + 50, position.height - 50, 40, 20), "I"))
            {
                SetItalic(true);
            }
            if (GUI.Button(new Rect(toolbarRect.x + 50, position.height - 25, 40, 20), "I_"))
            {
                SetItalic(false);
            }

            if (GUI.Button(new Rect(toolbarRect.x + 100, position.height - 50, 40, 40), "R"))
            {
                SetRuby();
            }
            m_CurRuby = EditorGUI.TextField(new Rect(toolbarRect.x + 150, position.height - 50, 190, 40), m_CurRuby, m_NormalStyle);

            if (GUI.Button(new Rect(toolbarRect.x + 400, position.height - 50, 40, 40), "S"))
            {
                SetFontSize();
            }
            m_FontSize = EditorGUI.IntField(new Rect(toolbarRect.x + 445, position.height - 50, 100, 40), m_FontSize, m_IntStyle);
            if (m_FontSize <= 5)
            {
                m_FontSize = 5;
            }

            Color color;
            for (int i = 0; i < 10; i++)
            {
                m_ColorRectList[i]=(new Rect(100 + i * 50, position.height - 115, 40, 40));
            }
            for (int i = 0; i < MMColor.MMColorList().Count; i++)
            {
                ColorUtility.TryParseHtmlString(MMColor.MMColorList()[i], out color);
                EditorGUI.DrawRect(m_ColorRectList[i], color);
            }
            EditorGUI.DrawRect(new Rect(toolbarRect.x + 95, position.height - 115, 1, 110), Color.white);
            EditorGUI.DrawRect(new Rect(1, position.height - 60, 600, 1), Color.white);
        }
        private void InitializeGUIStyle()
        {
            m_ColorField = new Color(1, 1, 1, 1);
            m_CharacterBG_N = EditorGUIUtility.Load("Assets/MirMirror/Icons/InputBG.png") as Texture2D;
            m_CharacterBG_C = EditorGUIUtility.Load("Assets/MirMirror/Icons/characterBg_C.png") as Texture2D;
            m_CharacterBG_R = EditorGUIUtility.Load("Assets/MirMirror/Icons/characterBg_R.png") as Texture2D;
            m_IntStyle = new GUIStyle();
            m_IntStyle.alignment = TextAnchor.MiddleCenter;
            m_IntStyle.normal.textColor = Color.white;
            m_IntStyle.fontSize = 15;
            m_IntStyle.normal.background = m_CharacterBG_N;
            m_ColorRectList = new List<Rect>();

            m_NormalStyle = new GUIStyle();
            m_NormalStyle.alignment = TextAnchor.MiddleCenter;
            m_NormalStyle.normal.textColor = Color.white;
            m_NormalStyle.fontSize = 15;
            m_NormalStyle.normal.background = m_CharacterBG_N;
            for (int i = 0; i < 10; i++)
            {
                m_ColorRectList.Add(new Rect(100 + i * 50, position.height - 115, 40, 40));
            }

        }
        private void UpadateDynamicStyle()
        {
            m_LcharacterIndex = -1;
            m_RcharacterIndex = -1;
            GUIStyle characterStyle = new GUIStyle();
            characterStyle.alignment = TextAnchor.MiddleCenter;
            characterStyle.normal.textColor = Color.white;
            characterStyle.fontSize = 20;
            characterStyle.normal.background = m_CharacterBG_N;

            m_CharacterStyleList = new List<GUIStyle>();
            for (int i = 0; i < m_ShowContent.Length; i++)
            {
                m_CharacterStyleList.Add(new GUIStyle(characterStyle));
            }

        }
        private void DrawHeader()
        {
            EditorGUI.DrawRect(new Rect(0, 0, position.width, 40), Color.black);
            if (GUI.Button(new Rect(5, 10, 50, 20), "Reset"))
            {
                Reset();
            }
            if (GUI.Button(new Rect(60, 10, 50, 20), "Apply"))
            {
                Compression();
            }
           

        }
        private void DrawContent()
        {
            if (m_ShowContent == null)
            {
                return;
            }
            int contentLength = m_ShowContent.Length;

            int characterIndex = 0;
            for (int i = 0; i < m_MaxRow; i++)
            {
                for (int j = 0; j < m_MaxCol; j++)
                {
                    characterIndex = i * m_MaxCol + j;
                    if (characterIndex >= contentLength)
                    {
                        return;
                    }

                    EditorGUI.LabelField(m_CharacterRects[characterIndex], m_ShowContent[characterIndex].ToString(), m_CharacterStyleList[characterIndex]);
                    m_IntervalList[characterIndex] = EditorGUI.FloatField(new Rect(10 + 50 * j, 140 + 120 * i, 40, 20), m_IntervalList[characterIndex], m_IntStyle);


                }
            }
        }
        private void DrawRuby()
        {
            for (int i = 0; i < m_RubyList.Count; i++)
            {
                Rect rect = new Rect((m_CharacterRects[m_RubyList[i].StartIndex].x + m_CharacterRects[m_RubyList[i].EndIndex].x) * 0.5f, m_CharacterRects[m_RubyList[i].StartIndex].y - 50, 20 * m_RubyList[i].Content.Length, 40);
                EditorGUI.LabelField(rect, m_RubyList[i].Content);
            }
        }
        private void UpdateCharacterBG()
        {
            if (m_LcharacterIndex >= 0 && m_RcharacterIndex >= 0)
            {
                m_IsSetCharacterTypeMode = true;
                for (int i = 0; i < m_CharacterStyleList.Count; i++)
                {
                    m_CharacterStyleList[i].normal.background = m_CharacterBG_N;
                    for (int j = 0; j < m_RubyList.Count; j++)
                    {
                        if (i >= m_RubyList[j].StartIndex && i <= m_RubyList[j].EndIndex)
                        {
                            m_CharacterStyleList[i].normal.background = m_CharacterBG_R;
                        }
                    }

                }
                if (m_LcharacterIndex <= m_RcharacterIndex)
                {
                    int characterCount = m_RcharacterIndex - m_LcharacterIndex + 1;
                    for (int i = m_LcharacterIndex; i < m_LcharacterIndex + characterCount; i++)
                    {
                        m_CharacterStyleList[i].normal.background = m_CharacterBG_C;

                    }
                }
                else
                {
                    int characterCount = m_LcharacterIndex - m_RcharacterIndex + 1;
                    for (int i = m_RcharacterIndex; i < m_RcharacterIndex + characterCount; i++)
                    {
                        m_CharacterStyleList[i].normal.background = m_CharacterBG_C;

                    }
                }
            }
            else
            {
                //´¦ÓÚÎ´Ñ¡Ôñ×´Ì¬
                m_IsSetCharacterTypeMode = false;
                for (int i = 0; i < m_CharacterStyleList.Count; i++)
                {
                    m_CharacterStyleList[i].normal.background = m_CharacterBG_N;
                    for (int j = 0; j < m_RubyList.Count; j++)
                    {
                        if (i >= m_RubyList[j].StartIndex && i <= m_RubyList[j].EndIndex)
                        {
                            m_CharacterStyleList[i].normal.background = m_CharacterBG_R;
                        }
                    }
                }

            }



        }
        private void ProcessEvent(Event e)
        {
            switch (e.type)

            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        for (int i = 0; i < m_ColorRectList.Count; i++)
                        {
                            if (m_ColorRectList[i].Contains(e.mousePosition))
                            {
                                SetColor(i);
                                e.Use();
                                break;
                            }

                        }
                     
                        for (int i = 0; i < m_ShowContent.Length; i++)
                        {


                            if (m_CharacterRects[i].Contains(e.mousePosition))
                            {
                                if (m_LcharacterIndex >= 0 && m_RcharacterIndex >= 0)
                                {
                                    m_LcharacterIndex = -1;
                                    m_RcharacterIndex = -1;

                                }
                                else if (m_LcharacterIndex >= 0 && m_RcharacterIndex < 0)
                                {
                                    m_RcharacterIndex = i;
                                }
                                else if (m_LcharacterIndex < 0 && m_RcharacterIndex >= 0)
                                {
                                    m_LcharacterIndex = i;
                                }
                                else
                                {
                                    m_LcharacterIndex = i;
                                }
                                e.Use();
                                break;
                            }

                        }
                    }
                    break;
                case EventType.MouseUp:
                    break;
                default:
                    break;
            }

        }
        private void SetColor(int _ColorIndex)
        {
            if (m_IsSetCharacterTypeMode == false)
            {
                return;
            }

            Color color;

            if (_ColorIndex < 0)
            {
                color = m_ColorField;
            }
            else
            {
                ColorUtility.TryParseHtmlString(MMColor.MMColorList()[_ColorIndex], out color);
            }
            if (m_LcharacterIndex <= m_RcharacterIndex)
            {
                int characterCount = m_RcharacterIndex - m_LcharacterIndex + 1;
                for (int i = m_LcharacterIndex; i < m_LcharacterIndex + characterCount; i++)
                {


                    m_CharacterStyleList[i].normal.textColor = color;
                    m_Colors[i] = "#" + ColorUtility.ToHtmlStringRGB(color);
                    
                }
            }
            else
            {
                int characterCount = m_LcharacterIndex - m_RcharacterIndex + 1;
                for (int i = m_RcharacterIndex; i < m_RcharacterIndex + characterCount; i++)
                {

                    m_CharacterStyleList[i].normal.textColor = color;
                    m_Colors[i] ="#"+ ColorUtility.ToHtmlStringRGB(color);
                   
                }
            }

                GUI.changed = true;

        }
        private void SetBlod(bool _Bool)
        {
            if (m_IsSetCharacterTypeMode == false)
            {
                return;
            }


            if (m_LcharacterIndex <= m_RcharacterIndex)
            {
                int characterCount = m_RcharacterIndex - m_LcharacterIndex + 1;
                for (int i = m_LcharacterIndex; i < m_LcharacterIndex + characterCount; i++)
                {

                    if (_Bool)
                    {

                        m_CharacterStyleList[i].fontStyle = FontStyle.Bold;
                        m_Blods[i] = true;
                    }
                    else
                    {
                        m_CharacterStyleList[i].fontStyle = FontStyle.Normal;
                        m_Blods[i] = false;
                    }
                    
                }
                UpdateFontStyle();
            }
            else
            {
                int characterCount = m_LcharacterIndex - m_RcharacterIndex + 1;
                for (int i = m_RcharacterIndex; i < m_RcharacterIndex + characterCount; i++)
                {

                    if (_Bool)
                    {

                        m_CharacterStyleList[i].fontStyle = FontStyle.Bold;
                        m_Blods[i] = true;
                    }
                    else
                    {
                        m_CharacterStyleList[i].fontStyle = FontStyle.Normal;
                        m_Blods[i] = false;
                    }
                  
                }
                UpdateFontStyle();
            }
            GUI.changed = true;
        }
        private void SetItalic(bool _Bool)
        {
            if (m_IsSetCharacterTypeMode == false)
            {
                return;
            }


            if (m_LcharacterIndex <= m_RcharacterIndex)
            {
                int characterCount = m_RcharacterIndex - m_LcharacterIndex + 1;
                for (int i = m_LcharacterIndex; i < m_LcharacterIndex + characterCount; i++)
                {

                    if (_Bool)
                    {

                        m_CharacterStyleList[i].fontStyle = FontStyle.Italic;
                        m_Italics[i] = true;
                    }
                    else
                    {
                        m_CharacterStyleList[i].fontStyle = FontStyle.Normal;
                        m_Italics[i] = false;
                    }
                    
                }
                UpdateFontStyle();
            }
            else
            {
                int characterCount = m_LcharacterIndex - m_RcharacterIndex + 1;
                for (int i = m_RcharacterIndex; i < m_RcharacterIndex + characterCount; i++)
                {

                    if (_Bool)
                    {

                        m_CharacterStyleList[i].fontStyle = FontStyle.Italic;
                        m_Italics[i] = true;
                    }
                    else
                    {
                        m_CharacterStyleList[i].fontStyle = FontStyle.Normal;
                        m_Italics[i] = false;
                    }
                    
                }
                UpdateFontStyle();
            }
            GUI.changed = true;
        }
        private void SetRuby()
        {
            if (m_IsSetCharacterTypeMode == false)
            {
                return;
            }


            RubyData rubyData = null;
            if (m_LcharacterIndex <= m_RcharacterIndex)
            {
                rubyData = new RubyData(m_LcharacterIndex, m_CurRuby);
                rubyData.EndIndex = m_RcharacterIndex;
            }
            else
            {
                rubyData = new RubyData(m_RcharacterIndex, m_CurRuby);
                rubyData.EndIndex = m_LcharacterIndex;

            }
            for (int i = 0; i < m_RubyList.Count; i++)
            {
                if (m_RubyList[i].StartIndex >= rubyData.StartIndex && m_RubyList[i].StartIndex <= rubyData.EndIndex)
                {
                    m_RubyList.RemoveAt(i);
                    i--;
                }
                else if (m_RubyList[i].EndIndex >= rubyData.StartIndex && m_RubyList[i].EndIndex <= rubyData.EndIndex)
                {
                    m_RubyList.RemoveAt(i);
                    i--;
                }
            }
            if (rubyData.Content.Length == 0)
            {
                return;
            }
            m_RubyList.Add(rubyData);
        }
        private void SetFontSize()
        {
            if (m_IsSetCharacterTypeMode == false)
            {
                return;
            }
            if (m_LcharacterIndex <= m_RcharacterIndex)
            {
                int characterCount = m_RcharacterIndex - m_LcharacterIndex + 1;
                for (int i = m_LcharacterIndex; i < m_LcharacterIndex + characterCount; i++)
                {



                    m_CharacterStyleList[i].fontSize = m_FontSize;
                    m_FontSizes[i] = m_FontSize;


                }
                UpdateFontStyle();
            }
            else
            {
                int characterCount = m_LcharacterIndex - m_RcharacterIndex + 1;
                for (int i = m_RcharacterIndex; i < m_RcharacterIndex + characterCount; i++)
                {



                    m_CharacterStyleList[i].fontSize = m_FontSize;
                    m_FontSizes[i] = m_FontSize;


                }
                UpdateFontStyle();
            }
            GUI.changed = true;
        }
        
        private void UpdateFontStyle()
        {
           
            for (int i = 0; i < m_CharacterStyleList.Count; i++)
            {
                m_CharacterStyleList[i].fontStyle = FontStyle.Normal;
            }
            for (int i = 0; i < m_Blods.Length; i++)
            {
                if (m_Blods[i] == true && m_Italics[i] == true)
                {
                    m_CharacterStyleList[i].fontStyle = FontStyle.BoldAndItalic;
                }
                else if (m_Blods[i] == true && m_Italics[i] == false)
                {
                    m_CharacterStyleList[i].fontStyle = FontStyle.Bold;
                }
                else if (m_Blods[i] == false && m_Italics[i] == true)
                {
                    m_CharacterStyleList[i].fontStyle = FontStyle.Italic;
                }
            }
            for (int i = 0; i < m_FontSizes.Length; i++)
            {
                m_CharacterStyleList[i].fontSize = m_FontSizes[i];
            }
            for (int i = 0; i < m_Colors.Length; i++)
            {


                ColorUtility.TryParseHtmlString(m_Colors[i], out Color color);

                m_CharacterStyleList[i].normal.textColor = color;
            }

        }

        private void Compression()
        {
            string text = m_ShowContent;
            int[] preRtf = new int[text.Length];
        
            #region

            for (int i = 0; i < m_RubyList.Count; i++)
            {
                int startIndex = m_RubyList[i].StartIndex;
                int insetIndex = startIndex;

                string insertString = "<r=" + m_RubyList[i].Content + ">";
                text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                for (int j = startIndex; j < preRtf.Length; j++)
                {
                    preRtf[j] += insertString.Length;
                }
                int endIndex = m_RubyList[i].EndIndex;
                insetIndex = endIndex + 1;
                insertString = "</r>";
                if (insetIndex >= preRtf.Length)
                {
                    text = text + insertString;
                }
                else
                {

                    text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                    for (int j = insetIndex; j < preRtf.Length; j++)
                    {
                        preRtf[j] += insertString.Length;
                    }
                }

            }
            #endregion
       
            #region
            bool startTrue = false;

            List<FontTypeData> startEnd = new List<FontTypeData>();
            for (int i = 0; i < m_Italics.Length; i++)
            {
                if (startTrue == true)
                {
                    if (m_Italics[i] == false)
                    {
                        startEnd[startEnd.Count-1].EndIndex = i - 1;
                        startTrue = false;

                    }
                    if (i == m_Italics.Length - 1)
                    {
                        startEnd[startEnd.Count - 1].EndIndex = i;
                    }

                }
                else
                {
                    if (m_Italics[i] == true)
                    {
                        startTrue = true;
                        startEnd.Add(new FontTypeData(i));
                        if (i == m_Italics.Length-1)
                        {
                            startEnd[startEnd.Count - 1].EndIndex = i;
                        }

                    }

                }

            }

            for (int i = 0; i < startEnd.Count; i++)
            {
                int insetIndex = startEnd[i].StartIndex;
                string insertString = "<i>";
                text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                for (int j = insetIndex; j < preRtf.Length; j++)
                {
                    preRtf[j] += insertString.Length;
                }
                insetIndex = startEnd[i].EndIndex + 1;
                insertString = "</i>";
                if (insetIndex>=preRtf.Length)
                {
                    text = text + insertString;
                }
                else
                {

                    text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                    for (int j = insetIndex; j < preRtf.Length; j++)
                    {
                        preRtf[j] += insertString.Length;
                    }
                }

            }
            #endregion

         
            #region
            startTrue = false;

            startEnd = new List<FontTypeData>();
            for (int i = 0; i < m_Blods.Length; i++)
            {
                if (startTrue == true)
                {
                    if (m_Blods[i] == false)
                    {
                        startEnd[startEnd.Count - 1].EndIndex = i - 1;
                        startTrue = false;
                  
                    }
                    if (i == m_Blods.Length - 1)
                    {
                        startEnd[startEnd.Count - 1].EndIndex = i;
                    }

                }
                else
                {
                    if (m_Blods[i] == true)
                    {
                        startTrue = true;
                        startEnd.Add(new FontTypeData(i));
                        if (i == m_Blods.Length - 1)
                        {
                            startEnd[startEnd.Count - 1].EndIndex = i;
                        }
                    }

                }

            }
            for (int i = 0; i < startEnd.Count; i++)
            {
                int insetIndex = startEnd[i].StartIndex;
                string insertString = "<b>";
                text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                for (int j = insetIndex; j < preRtf.Length; j++)
                {
                    preRtf[j] += insertString.Length;
                }
                insetIndex = startEnd[i].EndIndex + 1;
                insertString = "</b>";
                if (insetIndex >= preRtf.Length)
                {
                    text = text + insertString;
                }
                else
                {

                    text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                    for (int j = insetIndex; j < preRtf.Length; j++)
                    {
                        preRtf[j] += insertString.Length;
                    }
                }

            }
            #endregion
 
            #region
            startTrue = false;

            List<FontSize> startEndSize = new List<FontSize>();
            for (int i = 0; i < m_FontSizes.Length; i++)
            {
                if (startTrue == true)
                {
                    if (m_FontSizes[i] == 20)
                    {
                        startEndSize[startEndSize.Count - 1].EndIndex = i - 1;
                        startTrue = false;
            
                    }
                    if (i == m_FontSizes.Length - 1)
                    {
                        startEndSize[startEndSize.Count - 1].EndIndex = i;
                    }

                }
                else
                {
                    if (m_FontSizes[i] != 20)
                    {
                        startTrue = true;
                        startEndSize.Add(new FontSize(i, m_FontSizes[i]));
                        if (i == m_FontSizes.Length - 1)
                        {
                            startEndSize[startEndSize.Count - 1].EndIndex = i;
                        }
                    }

                }

            }
            for (int i = 0; i < startEndSize.Count; i++)
            {
                int insetIndex = startEndSize[i].StartIndex;
                string insertString = "<size="+ startEndSize[i] .Size+ ">";
                text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                for (int j = insetIndex; j < preRtf.Length; j++)
                {
                    preRtf[j] += insertString.Length;
                }
                insetIndex = startEndSize[i].EndIndex + 1;
                insertString = "</size>";
                if (insetIndex >= preRtf.Length)
                {
                    text = text + insertString;
                }
                else
                {

                    text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                    for (int j = insetIndex; j < preRtf.Length; j++)
                    {
                        preRtf[j] += insertString.Length;
                    }
                }


            }
            #endregion
      
            #region
            startTrue = false;
            string preColor= "#FFFFFF";

            List<FontColor> startEndColor = new List<FontColor>();
            for (int i = 0; i < m_Colors.Length; i++)
            {

                if (startTrue == true)
                {
                    if (m_Colors[i] == "#FFFFFF"||preColor!=m_Colors[i])
                    {
                        startEndColor[startEndColor.Count - 1].EndIndex = i - 1;
                        preColor = "#FFFFFF";
                        startTrue = false;
                    }
                    if (i == m_Colors.Length - 1)
                    {
                        startEndColor[startEndColor.Count - 1].EndIndex = i;
                    }

                }
                else
                {
                    if (m_Colors[i] != "#FFFFFF")
                    {

                        startTrue = true;
                        preColor = m_Colors[i];
                        startEndColor.Add(new FontColor(i, m_Colors[i]));
                        if (i == m_Colors.Length - 1)
                        {
                            startEndColor[startEndColor.Count - 1].EndIndex = i;
                        }
                    }

                }

            }

            for (int i = 0; i < startEndColor.Count; i++)
            {
                int insetIndex = startEndColor[i].StartIndex;

                string insertString = "<color=" + startEndColor[i].Color + ">";

                text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                for (int j = insetIndex; j < preRtf.Length; j++)
                {
                    preRtf[j] += insertString.Length;
                }

                insetIndex = startEndColor[i].EndIndex + 1;
                insertString = "</color>";


                if (insetIndex >= preRtf.Length)
                {
                    text = text + insertString;
                }
                else
                {

                    text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                    for (int j = insetIndex; j < preRtf.Length; j++)
                    {
                        preRtf[j] += insertString.Length;
                    }
                }

            }
            #endregion
    
            #region


            for (int i = 0; i < m_IntervalList.Length; i++)
            {

                if (m_IntervalList[i] >= 0)
                {
                    int insetIndex = i+1;
                    string insertString = "<" + m_IntervalList[i].ToString() + ">";
                    if (insetIndex>=preRtf.Length)
                    {
                        text = text + insertString;
                    }
                    else
                    {
                        text = text.Insert(insetIndex + preRtf[insetIndex], insertString);
                        for (int j = insetIndex; j < preRtf.Length; j++)
                        {
                            preRtf[j] += insertString.Length;
                        }

                    }

                }

            }

            #endregion

            m_Node.Words[m_DialogueID] = text;
            this.Close();
        }

    }
    public static class MMColor
    {
        public static string aqua = "#00ffffff";
        public static string brown = "#a52a2aff";
        public static string fuchsia = "#ff00ffff";
        public static string lightblue = "#add8e6ff";
        public static string lime = "#00ff00ff";
        public static string olive = "#808000ff";
        public static string red = "#ff0000ff";
        public static string yellow = "#ffff00ff";
        public static string white = "#ffffffff";
        public static string orange = "#ffa500ff";

        public static List<string> MMColorList()
        {
            List<string> ColorList = new List<string>();
            ColorList.Add(aqua);
            ColorList.Add(brown);
            ColorList.Add(fuchsia);
            ColorList.Add(lightblue);
            ColorList.Add(lime);
            ColorList.Add(olive);
            ColorList.Add(red);
            ColorList.Add(yellow);
            ColorList.Add(white);
            ColorList.Add(orange);
            return ColorList;
        }


    }
    public class FontTypeData
    {
        private int m_StartIndex;
        public int StartIndex { get { return m_StartIndex; } }
        private int m_EndIndex;
        public int EndIndex { get { return m_EndIndex; } set { m_EndIndex = value; } }
        public FontTypeData(int _StartIndex)
        {
            m_StartIndex = _StartIndex;
        }


    }
    public class FontColor
    {
        private int m_StartIndex;
        public int StartIndex { get { return m_StartIndex; } }
        private int m_EndIndex;
        public int EndIndex { get { return m_EndIndex; } set { m_EndIndex = value; } }
        private string m_Color;
        public string Color { get { return m_Color; } set { m_Color = value; } }
        public FontColor(int _StartIndex, string _Color)
        {
            m_StartIndex = _StartIndex;
            m_Color = _Color;
        }


    }
    public class FontSize
    {
        private int m_StartIndex;
        public int StartIndex { get { return m_StartIndex; } }
        private int m_EndIndex;
        public int EndIndex { get { return m_EndIndex; } set { m_EndIndex = value; } }
        private int m_Size;
        public int Size { get { return m_Size; } set { m_Size = value; } }
        public FontSize(int _StartIndex, int _Size)
        {
            m_StartIndex = _StartIndex;
            m_Size = _Size;
        }
    }
}
