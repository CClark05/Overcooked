using System;
using UnityEngine;

public class ShakeTween : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float timeBetweenShakes;
    [SerializeField] private bool moveLocal;
    private float originalXPos;
    private void OnEnable()
    {
        originalXPos = moveLocal ? GetComponent<RectTransform>().localPosition.x : GetComponent<Transform>().position.x;
        if (moveLocal)
        {
            LeanTween.moveLocalX(gameObject, originalXPos - distance, timeBetweenShakes).setOnComplete(() =>
            {
                LeanTween.moveLocalX(gameObject, originalXPos + distance * 2, timeBetweenShakes * 2).setLoopPingPong();
            });
            return;
        }
        LeanTween.moveX(gameObject, originalXPos - distance, timeBetweenShakes).setOnComplete(() =>
        {
            LeanTween.moveX(gameObject, originalXPos + distance * 2, timeBetweenShakes * 2).setLoopPingPong();
        });
        
    }
    
    
}