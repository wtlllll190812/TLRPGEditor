using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class MyMouseEvent : MonoBehaviour,IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{

    

    [Serializable]
    public class MyMouseAllEvent : UnityEvent { }
    public MyMouseAllEvent m_MyOnClickLeft = new MyMouseAllEvent();
    public MyMouseAllEvent m_MyOnClickRight = new MyMouseAllEvent();
    public MyMouseAllEvent m_MyOnEnter = new MyMouseAllEvent();
    public MyMouseAllEvent m_MyOnExit = new MyMouseAllEvent();
    public MyMouseAllEvent m_MyPointerDown = new MyMouseAllEvent();
    public MyMouseAllEvent m_MyPointerUp = new MyMouseAllEvent();
    protected MyMouseEvent()
    { }

    #region
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        

        if (eventData.button==PointerEventData.InputButton.Left)
        {

            PressLeft();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {

            PressRight();
        }


        
    }
    
    private void PressLeft()
    {

        m_MyOnClickLeft.Invoke();
    }
    private void PressRight()
    {
        m_MyOnClickRight.Invoke();
    }

    #endregion;
  
    #region
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        Enter();
    }
    private void Enter()
    {             
        m_MyOnEnter.Invoke();
    }
    #endregion

  
    #region
    public void OnPointerExit(PointerEventData eventData)
    {
       
        Exit();
    }
    private void Exit()
    {      
        m_MyOnExit.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        m_MyPointerDown.Invoke();

    }
    public void OnPointerUp(PointerEventData eventData)
    {

        m_MyPointerUp.Invoke();
    }

    #endregion
}

