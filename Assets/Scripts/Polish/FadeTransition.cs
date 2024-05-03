using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using AYellowpaper;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour, ITransition
{
    [SerializeField] float fadeTime = 0.4f;
    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void FadeIn()
    {
        LeanTween.value(gameObject, f => canvasGroup.alpha = f, 1, 0, fadeTime);

    }

    private void FadeOut(Action OnComplete)
    {
        LeanTween.value(gameObject, f => canvasGroup.alpha = f, 0, 1, fadeTime).setOnComplete(() =>
        {
            OnComplete?.Invoke();
        });
        
    }

    public void OnExit(Action OnComplete) => FadeOut(OnComplete);
    public void OnEnter() => FadeIn();
}