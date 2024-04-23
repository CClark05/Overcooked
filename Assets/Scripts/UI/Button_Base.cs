using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class Button_Base : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public abstract void OnMouseEnter();
    public abstract void OnMouseLeave();
    public UnityEvent OnClick;
    protected RectTransform rectTransform;
    private Image image;
    private Color originalColor;

    protected void OnEnable()
    {
        image.color = originalColor;
    }
    protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData) => OnMouseEnter();
    public void OnPointerExit(PointerEventData eventData) => OnMouseLeave();

    protected virtual void OnClickAnimation()
    {
        image.color = new Color(0.8f, 0.8f, 0.8f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickAnimation();
        OnClick?.Invoke();
    }
    
}
