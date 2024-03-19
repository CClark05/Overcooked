using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    [SerializeField] private bool looping;
    [SerializeField] private float duration;
    [SerializeField] private float scaleFactor;
    [SerializeField] private LeanTweenType easeType;
    private void OnEnable()
    {
        if (looping) LeanTween.scale(gameObject, new Vector3(scaleFactor, scaleFactor, scaleFactor), duration).setLoopPingPong().setEase(easeType);
        else
        {
            LeanTween.scale(gameObject, new Vector3(scaleFactor, scaleFactor, scaleFactor), duration).setEase(easeType);
        }

    }
}
