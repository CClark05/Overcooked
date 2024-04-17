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

    public Sprite GetSpriteFromNumber(int number)
    {
        var sprite = sprites[number];
        if (sprite == null)
        {
            Debug.LogError("Sprite is null");
        }
        return sprite;
    }
    public int GetSpriteNumber(Sprite sprite)
    {
        for(int i = 0; i<sprites.Length; i++){
            if (sprites[i].Equals(sprite))
            {
                return i;
            }
        }
        Debug.LogError("Sprite invalid");
        return -1;
    }
}
