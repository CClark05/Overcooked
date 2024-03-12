using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public static ScoreCalculator Instance;
    public int score { get; private set; }
    public int lastScore { get; private set; }
    public Action OnScoreChanged;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CustomerSpawner.Instance.OnMoodAdded += CalculateScore;
        CustomerData.OnLostPatience += (FoodRecipeSO) =>
        {
            lastScore = score;
            score -= 30;
            OnScoreChanged?.Invoke();
        };
    }
    private void CalculateScore(CustomerData.Moods mood)
    {
        lastScore = score;
        switch (mood)
        {
            case CustomerData.Moods.Content:
                score += 50;
                break;
            case CustomerData.Moods.Impatient:
                score += 40;
                break;
            case CustomerData.Moods.Frustrated:
                score += 30;
                break;
            case CustomerData.Moods.Angry:
                score += 20;
                break;
        }
        OnScoreChanged?.Invoke();
    }
}
