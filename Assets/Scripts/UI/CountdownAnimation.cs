using System;
using TMPro;
using UnityEngine;


public class CountdownAnimation : MonoBehaviour
{
    private Vector3 originalPosition;
    [SerializeField] private TextMeshProUGUI numberText;
    private void Awake()
    {
        originalPosition = GetComponent<RectTransform>().localPosition;
    }

    private void Start()
    {
        GetComponent<CountdownUI>().OnNumberChanged += MoveAnimation;
    }

    private void MoveAnimation()
    {
        GetComponent<RectTransform>().localPosition = originalPosition;
        GetComponent<RectTransform>().localScale = Vector3.one;
        LeanTween.cancel(gameObject);
        float moveTime = 0.35f;
        float scaleTime = 0.4f;
        LeanTween.value(gameObject, f => numberText.color = new Color(1, 1, 1, f), 0, 1, 0.2f);
        LeanTween.moveLocalY(gameObject, 0, moveTime).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, Vector3.zero, scaleTime).setDelay(0.2f).setEaseInOutCubic();
        }).setEaseInCubic();
    }
}