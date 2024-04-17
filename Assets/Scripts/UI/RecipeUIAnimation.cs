using System;
using UnityEngine;

public class RecipeUIAnimation : MonoBehaviour
{
    private float originalXPos;

    private void Start() => GetComponent<RecipeUI>().OnAboutToExpire += ShakeAnimation;

    private void ShakeAnimation()
    {

        originalXPos = GetComponent<RectTransform>().localPosition.x;
        float distance = 1.5f;
        float time = 0.04f;
        LeanTween.moveLocalX(gameObject, originalXPos - distance, time).setOnComplete(() =>
        {
            LeanTween.moveLocalX(gameObject, originalXPos + distance * 2, time * 2).setLoopPingPong();
        });
    }
}