using System;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace SaveSystem.New
{
    public class NewSaveManager : MonoBehaviour
    {
        
        public static NewSaveManager Instance { get; private set; }
        public bool HasNewHighScore { get; private set; }
        private void Awake()
        {
            Instance = this;
            
        }

        private void Start()
        {
            SaveLoadSystem.Instance.Load();
            
            ScoreCalculator.Instance.SetHighScore(GetComponent<LevelSave>().highScore);
            
            GameManager.Instance.OnGameEnded += () =>
            {
                if (ScoreCalculator.Instance.score > ScoreCalculator.Instance.highScore)
                {
                    GetComponent<LevelSave>().highScore = ScoreCalculator.Instance.score;
                    ScoreCalculator.Instance.SetHighScore(GetComponent<LevelSave>().highScore);
                    SaveLoadSystem.Instance.Save();
                    HasNewHighScore = true;
                }
            };
        }
    }
}