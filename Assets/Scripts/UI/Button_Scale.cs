using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Button_Scale : Button_Base
{
    [SerializeField] private float scaleFactor;
    [SerializeField] private float animationDuration;
    [SerializeField] private Sprite pressedButtonSprite;
    private Sprite originalSprite;

    private new void Awake()
    {
        base.Awake();
        originalSprite = GetComponent<Image>().sprite;
    }

    private new void OnEnable()
    {
        base.OnEnable();
        GetComponent<RectTransform>().localScale = Vector3.one;
    }
    public override void OnMouseEnter()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(scaleFactor, scaleFactor, scaleFactor), animationDuration).setIgnoreTimeScale(true).setEaseOutCubic();
    }
    public override void OnMouseLeave()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), animationDuration * 0.75f).setIgnoreTimeScale(true).setEaseOutSine();
    }

    protected override void OnMouseClick()
    {
        StartCoroutine(ClickAnimation());
    }

    private IEnumerator ClickAnimation()
    {
        float delay = 0.1f;
        GetComponent<Image>().sprite = pressedButtonSprite;
        yield return new WaitForSecondsRealtime(delay);
        GetComponent<Image>().sprite = originalSprite;
    }
}
    

