using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class RubyData
{
    private int m_StartIndex;
    public int StartIndex { get { return m_StartIndex; } }
    private int m_EndIndex;
    public int EndIndex { get { return m_EndIndex; }set { m_EndIndex = value; } }

    private string m_Content;
    public string Content { get { return m_Content; } }

    public RubyData(int _StartIndex, string _Content)
    {
        m_StartIndex = _StartIndex;
        m_Content = _Content;
        m_EndIndex = m_StartIndex;
    }
}

public class AdvancedTextPreprocessor : ITextPreprocessor
{

    private Dictionary<int, float> m_IntervalDictionary;
    public Dictionary<int, float> IntervalDictionary { get { return m_IntervalDictionary; }set { m_IntervalDictionary = value; } }
    private  List<RubyData> m_RubyList;
    public List<RubyData> RubyList { get { return m_RubyList; } }
    public AdvancedTextPreprocessor()
    {
        m_IntervalDictionary = new Dictionary<int, float>();
        m_RubyList = new List<RubyData>();
    }
    public bool TryGetRubyStartFrom(int _Index,out RubyData _Data)
    {
        _Data = new RubyData(0,"");
        foreach (var item in RubyList)
        {
            if (item.StartIndex==_Index)
            {
                _Data = item;
                return true;
            }
        }
        return false;
    }
    public string PreprocessText(string _Text)
    {

        m_IntervalDictionary.Clear();
        m_RubyList.Clear();
        string text = _Text;
        string pattern = "<.*?>";
        Match match = Regex.Match(text,pattern);

        while (match.Success)
        {


            string label = match.Value.Substring(1, match.Length - 2);
            if (float.TryParse(label, out float result))
            {
                IntervalDictionary.Add(match.Index - 1, result);
            }
            else if(Regex.IsMatch(label,"^r=.*"))
            {
                m_RubyList.Add(new RubyData(match.Index,label.Substring(2)));

            }
            else if(label=="/r")
            {
                if (m_RubyList.Count>0)
                {
                    m_RubyList[m_RubyList.Count - 1].EndIndex = match.Index - 1;
                }
            }
            text = text.Remove(match.Index, match.Length);
            if (Regex.IsMatch(label, "^sprite=.*"))
            {
                text = text.Insert(match.Index, " ");

            }
            if (text.Length == 0)
            {
                break;
            }
            match = Regex.Match(text, pattern);
        }

        text = _Text;
        pattern = @"<(\d+)(\.\d+)?>|(</r>|(<r=.*?>))";
        text = Regex.Replace(text,pattern,"");


        return text;
    }
}

public class AdvancedText : TextMeshProUGUI
{
    private float m_DefalutIntervalTime = 0.2f;
    private float m_NowIntervalTime;

    private bool m_IsTypingOver = false;
    public bool IsTypingOver { get { return m_IsTypingOver; }set { m_IsTypingOver = value; } }
 


    private AdvancedTextPreprocessor m_AdvancedTextPreprocessor => (textPreprocessor as AdvancedTextPreprocessor);
    public AdvancedText()
    {
        textPreprocessor = new AdvancedTextPreprocessor();
       
    }
    private void SetRubyText(RubyData data)
    {
        GameObject rubyPrefab = Resources.Load<GameObject>("RubyPrefab");
        GameObject rubyObj = Instantiate(rubyPrefab,transform);
        rubyObj.GetComponent<TextMeshProUGUI>().SetText(data.Content);
        rubyObj.GetComponent<TextMeshProUGUI>().color = textInfo.characterInfo[data.StartIndex].color;
        rubyObj.transform.localPosition = (textInfo.characterInfo[data.StartIndex].topLeft + textInfo.characterInfo[data.EndIndex].topRight) / 2;
    }

    public void OverleapDialogue()
    {
        m_NowIntervalTime = 0;
        m_AdvancedTextPreprocessor.IntervalDictionary.Clear();
    }
    public void ShowTextByTyping(string _Content)
    {
        m_IsTypingOver = false;
        SetText(_Content);
        StartCoroutine(Typing());
    }
    IEnumerator Typing()
    {
        m_NowIntervalTime = m_DefalutIntervalTime;

        ForceMeshUpdate();
        for (int i = 0; i < m_characterCount; i++)
        {
            SetSingleCharacterAlpha(i, 0);
        }



        int typingIndex = 0;
        while (typingIndex<m_characterCount)
        {
   

            StartCoroutine(FadeInCharacter(typingIndex));


            if (m_AdvancedTextPreprocessor.IntervalDictionary.TryGetValue(typingIndex, out float result))
            {
                yield return new WaitForSecondsRealtime(result);
            }
            else
            {
                yield return new WaitForSecondsRealtime(m_NowIntervalTime);
            }
            typingIndex++;
            

        }
 
        float time=0;
        while (time<0.5f)
        {
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        m_IsTypingOver = true;


        
    }
    IEnumerator FadeInCharacter(int _Index,float _duration=0.5f)
    {
        if (m_AdvancedTextPreprocessor.TryGetRubyStartFrom(_Index,out RubyData data))
        {
            SetRubyText(data);
        }



        if (_duration<=0)
        {
            SetSingleCharacterAlpha(_Index,255);
        }
        else
        {
            float timer = 0;
            while (timer<_duration)
            {
                
                timer = Mathf.Min(_duration,timer+Time.unscaledDeltaTime);
                SetSingleCharacterAlpha(_Index,(byte)(timer/_duration*255));
                yield return null;
            }
        }


        
    }

    public void SetSingleCharacterAlpha(int _Index, byte _Alpha)
    {
        TMP_CharacterInfo charInfo = textInfo.characterInfo[_Index];
        if (!charInfo.isVisible)
        {
            return;
        }
        int matIndex = charInfo.materialReferenceIndex;
        int vertIndex = charInfo.vertexIndex;
        for (int i = 0; i < 4; i++)
        {
            textInfo.meshInfo[matIndex].colors32[vertIndex + i].a = _Alpha;
        }
        UpdateVertexData();
    }
}
