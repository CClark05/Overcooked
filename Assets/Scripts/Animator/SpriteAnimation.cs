using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Sprite Animation")] 
public class SpriteAnimation : ScriptableObject
{
    public Sprite[] sprites;
    public float frameRate;
    public bool isLooping;
    public Action OnComplete;
    public bool isLocked;
    public bool lockMovement;

    private bool isDone;
    public bool GetIsDone()
    {
        return isDone;
    }
    public void SetIsDone(bool isDone)
    {
        this.isDone = isDone;
    }
}
