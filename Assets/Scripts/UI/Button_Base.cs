using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Button_Base : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public abstract void OnMouseEnter();
    public abstract void OnMouseLeave();
    public Action ClickFunction;
    protected RectTransform rectTransform;
    private Image image;
    protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData) => OnMouseEnter();
    public void OnPointerExit(PointerEventData eventData) => OnMouseLeave();
    public virtual void OnClickAnimation()
    {
        image.color = new Color(0.8f, 0.8f, 0.8f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickAnimation();
        ClickFunction?.Invoke();
    }
    
}
