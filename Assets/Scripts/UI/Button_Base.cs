using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class Button_Base : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public abstract void OnMouseEnter();
    public abstract void OnMouseLeave();
    public UnityEvent OnClick;
    protected RectTransform rectTransform;
    private Image image;
    private Color originalColor;
    [SerializeField] private float clickDelay;

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

    protected virtual void OnMouseClick()
    {
        image.color = new Color(0.8f, 0.8f, 0.8f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(ClickDelay());
    }

    private IEnumerator ClickDelay()
    {
        OnMouseClick();
        yield return new WaitForSecondsRealtime(clickDelay);
        OnClick?.Invoke();
    }
    
}
