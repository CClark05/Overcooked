using System;
using UnityEngine;


public class ProgressBarAnimation : MonoBehaviour
{
    [SerializeField] private ProgressBarUI progressBarUI;
    private float scaleTime = 0.3f;
    private void Start()
    {
        progressBarUI.OnShowBar += () =>
        {
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), scaleTime).setEaseOutCubic();
        };
        progressBarUI.OnHideBar += () =>
        {
            GetComponent<ShakeTween>().enabled = false;
            LeanTween.scale(gameObject, Vector3.zero, scaleTime).setEaseInCubic();
        };
    }
}