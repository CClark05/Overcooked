using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }
        public Action OnNewHighScore;
        public bool HasNewHighScore { get; private set; }

        // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
        private void Awake()
        {
            Instance = this;
            HasNewHighScore = false;
        }

        private void Start()
        {
            GameManager.Instance.OnGameStarted += () =>
            {
                if(SaveData.TryLoadHighScore())
                    ScoreCalculator.Instance.SetHighScore(SaveData.Instance.GetHighScore(GameManager.Instance.LevelData.level));
            };
            GameManager.Instance.OnGameEnded += () =>
            {
                if (SaveData.Instance.AddHighScore(GameManager.Instance.LevelData.level, ScoreCalculator.Instance.score))
                {
                    ScoreCalculator.Instance.SetHighScore(SaveData.Instance.GetHighScore(GameManager.Instance.LevelData.level));
                    SaveData.Instance.SaveHighScore();
                    OnNewHighScore?.Invoke();
                    HasNewHighScore = true;
                }
            };
        }
    }
}