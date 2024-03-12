using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private Color originalColor;
    private void Awake()
    {
        originalColor = scoreText.color; 
    }
    private void Start()
    {
        ScoreCalculator.Instance.OnScoreChanged += () =>
        {
            AnimateText(ScoreCalculator.Instance.lastScore, ScoreCalculator.Instance.score);
            if(ScoreCalculator.Instance.score < 0)
            {
                scoreText.color = new Color(126f / 255f, 38f / 255f, 51f / 255f);
            }
            else
            {
                scoreText.color = originalColor;
            }
        };
    }
    private void AnimateText(int startingScore, int endingScore)
    {
        float duration = 0.8f;
        LeanTween.value(gameObject, callback, startingScore, endingScore, duration);
        void callback(float value)
        {
            scoreText.text = (Mathf.RoundToInt(value)).ToString();
        }
    }

}
