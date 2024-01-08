using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprites : MonoBehaviour
{
    public static TileSprites Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private Sprite[] sprites;
    public Sprite[] GetSprites()
    {
        return sprites;
    }
}
