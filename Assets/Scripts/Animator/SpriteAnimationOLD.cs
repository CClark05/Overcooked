using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Sprite Animation")]
public class SpriteAnimationOLD : ScriptableObject
{
    public Sprite[] sprites;
    public float frameRate;
    public bool looping;
    public bool locked;
}
