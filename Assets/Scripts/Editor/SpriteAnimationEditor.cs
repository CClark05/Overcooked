using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteAnimation))]
public class SpriteAnimationEditor : Editor
{
    SerializedProperty spritesProperty;
    SerializedProperty frameRateProperty;
    SerializedProperty isLoopingProperty;
    SerializedProperty isLockedProperty;
    SerializedProperty lockMovementProperty;
    // SerializedProperty onCompleteProperty; // Uncomment if using UnityEvent for OnComplete

    private void OnEnable()
    {
        // Setup the SerializedProperties
        spritesProperty = serializedObject.FindProperty("sprites");
        frameRateProperty = serializedObject.FindProperty("frameRate");
        isLoopingProperty = serializedObject.FindProperty("isLooping");
        isLockedProperty = serializedObject.FindProperty("isLocked");
        lockMovementProperty = serializedObject.FindProperty("lockMovement");
        // onCompleteProperty = serializedObject.FindProperty("OnComplete"); // Uncomment for UnityEvent
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(spritesProperty, new GUIContent("Sprites"), true); // True to make it editable
        EditorGUILayout.PropertyField(frameRateProperty, new GUIContent("Frame Rate"));
        EditorGUILayout.PropertyField(isLoopingProperty, new GUIContent("Is Looping"));

        // Conditionally show properties
        if (!isLoopingProperty.boolValue) // If isLooping is false
        {
            // EditorGUILayout.PropertyField(onCompleteProperty, new GUIContent("On Complete")); // Uncomment for UnityEvent
            EditorGUILayout.PropertyField(isLockedProperty, new GUIContent("Is Locked"));
            EditorGUILayout.PropertyField(lockMovementProperty, new GUIContent("Lock Movement"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}