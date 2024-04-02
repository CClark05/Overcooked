using SaveSystem;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveManager))]
public class SaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Reset High Score"))
        {
            SaveData.Instance.ResetHighScore();
            Debug.Log("Reset High Score");
        }

    }
}