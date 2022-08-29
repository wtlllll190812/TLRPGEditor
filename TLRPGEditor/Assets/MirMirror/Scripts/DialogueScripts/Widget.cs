using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Widget : MonoBehaviour
{
    private CanvasGroup m_CanvasGroup;
    [SerializeField]
    private AnimationCurve m_FadingCurve=AnimationCurve.EaseInOut(0,0,1,1);
    private void Awake()
    {

        m_CanvasGroup = this.GetComponent<CanvasGroup>();
    }
    private Coroutine m_FadeCoroutine=null;
    public void Fade(float _Opacity, float _Duration, Action _OnFinished)
    {
        if (_Duration<=0)
        {
            m_CanvasGroup.alpha = _Opacity;
            _OnFinished?.Invoke();
        }
        else
        {
            if (m_FadeCoroutine!=null)
            {

                StopCoroutine(m_FadeCoroutine);
            }
            m_FadeCoroutine = StartCoroutine(Fading(_Opacity,_Duration,_OnFinished));
        }
    }
    private IEnumerator Fading(float _Opacity, float _Duration, Action _OnFinished)
    {
        float timer = 0;
        float start = m_CanvasGroup.alpha;
        while (timer<_Duration)
        {
            timer = Math.Min(_Duration,timer+Time.unscaledDeltaTime);
            m_CanvasGroup.alpha = Mathf.Lerp(start,_Opacity,m_FadingCurve.Evaluate(timer/_Duration));
            yield return null;
        }
        _OnFinished?.Invoke();
    }
    public bool IsVisible()
    {
        if (m_CanvasGroup.alpha>=0.9f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
