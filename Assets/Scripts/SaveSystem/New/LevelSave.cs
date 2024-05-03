using System;
using UnityEngine;

namespace SaveSystem.New
{
    public class LevelSave : MonoBehaviour, ISaveable
    {
        public bool isLocked;
        public int highScore;
        public int stars;
        
        public object CaptureState()
        {
            return new SaveData()
            {
                isLocked = this.isLocked,
                highScore = this.highScore,
                stars = this.stars
            };
        }
        
        public void RestoreState(object state)
        {
            var saveData = (SaveData)state;
            highScore = saveData.highScore;
            stars = saveData.stars;
        } 

        [Serializable]
        private struct SaveData
        {
            public bool isLocked;
            public int highScore;
            public int stars;
        }
    }
}