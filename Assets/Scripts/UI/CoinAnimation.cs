using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] coinSprites;
    [SerializeField] private Image coinImage;
    private void Start()
    {
        ScoreCalculator.Instance.OnScoreChanged += () =>
        {
            float duration = 0.6f;
            LeanTween.value(gameObject, Callback, 0, coinSprites.Length - 1, duration);
            void Callback(float value)
            {
                coinImage.sprite = coinSprites[Mathf.RoundToInt(value)];
            }
        };
    }
}
