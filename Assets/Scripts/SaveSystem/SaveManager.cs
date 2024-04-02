using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        private static SaveManager instance;
        // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
        public static SaveManager Instance => instance ??= new SaveManager();
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
                }
            };
        }
    }
}