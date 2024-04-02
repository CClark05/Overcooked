using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIAnimation : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image background;
    [SerializeField] private GameObject goldStarPrefab;
    private void Awake()
    {
        GetComponent<GameOverUI>().OnShowUI += () =>
        {
            LeanTween.value(gameObject, Callback, 0, 240 / 255f, 0.25f).setDelay(0.5f).setOnComplete(() =>
            {
                LeanTween.moveY(panel.GetComponent<RectTransform>(), -34, 0.7f).setEase(LeanTweenType.easeOutSine).setOnComplete(GoldStarAnimation);
                
            });
        
            void Callback(float value)
            {
                background.color = new Color(0, 0, 0, value);
            }
        };
        

        
    }
    private void GoldStarAnimation()
    {
        float delay = 0;
        float duration = 0.45f;
        foreach (var star in GetComponent<GameOverUI>().GetGoldStars())
        {
            TweenStar(star, delay, duration);
            delay += duration + 0.2f;
        }
    }
    private void TweenStar(GameObject star, float delay, float duration)
    {
        FunctionTimer.Create(() =>
        {
            GameObject newStar = Instantiate(goldStarPrefab, star.transform);
            LeanTween.scale(newStar, new Vector3(1.4f, 1.4f, 1), duration).setEaseOutCubic().setOnComplete(() =>
                LeanTween.scale(newStar, new Vector3(1, 1, 1), 0.1f).setEaseOutSine().setDelay(0.01f));

        }, delay);

    }
}
