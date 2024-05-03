using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem.New
{
    public class SaveLoadSystem : MonoBehaviour
    {
        public static SaveLoadSystem Instance { get; private set; }
        private string path;
        
        private void Awake()
        {
            Instance = this;
            path = $"{Application.persistentDataPath}/save.txt";
        }
        [ContextMenu("Save")]
        public void Save()
        {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }
        [ContextMenu("Load")]
        public void Load()
        {
            var state = LoadFile();
            RestoreState(state);
        }
        [ContextMenu("Delete")]
        public void Delete()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            
            using (var stream = File.Open(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(object state)
        {
            using (var stream = File.Open(path, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsByType<SaveableEntity>(FindObjectsSortMode.None))
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsByType<SaveableEntity>(FindObjectsSortMode.None))
            {
                if (state.TryGetValue(saveable.Id, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}