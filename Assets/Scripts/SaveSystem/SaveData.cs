using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        private static SaveData instance;
        public static SaveData Instance => instance ??= new SaveData();
        //level, high score
        private Dictionary<int, int> highScores = new();
        private const string highScoreFile = "/highScores";

        public bool AddHighScore(int level, int score)
        {
            if (highScores.TryGetValue(level, out int highScore))
            {
                if (score > highScore)
                {
                    highScores[level] = score;
                    return true;
                }

                return false;
            }
            else
            {
                highScores[level] = score;
                return true;
            }
        }

        public int GetHighScore(int level)
        {
            if (!highScores.ContainsKey(level))
            {
                Debug.LogError("Level does not exist");
                return -1;
            }
            return highScores[level];
        }

        public void SaveHighScore()
        {
            SerializationManager.Save(highScoreFile, this);
        }

        public static bool TryLoadHighScore()
        {
            string path = Application.persistentDataPath + "/saves" + highScoreFile + ".save";
            SaveData data = SerializationManager.Load(path) as SaveData;
            if (data == null)
            {
                Debug.LogError("Save data not found");
                return false;
            }
            instance = data;
            return true;
        }

        public void ResetHighScore()
        {
            highScores[GameManager.Instance.LevelData.level] = 0;
            SaveHighScore();
        }
        
    }

}