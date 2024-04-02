using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public class SerializationManager
    {
        public static bool Save(string saveName, object saveData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            string path = Application.persistentDataPath + "/saves" + saveName + ".save";
            FileStream file = File.Create(path);
            formatter.Serialize(file, saveData);
            file.Close();
            return true;
        }

        public static object Load(string path)
        {
            if (!File.Exists(path)) return null;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            try
            {
                object save = formatter.Deserialize(file);
                file.Close();
                return save;
            }
            catch
            {
                Debug.LogError("Failed to load file at " + path);
                file.Close();
                return null;
            }
        }

    }
}