using SuperTiled2Unity.Editor;
using SuperTiled2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileToPrefab : MonoBehaviour
{

    public int spriteNumber;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private bool isEdge;
    public void SpriteNumber(int num)
    {
        spriteNumber = num;
    }
    public void IsEdge(bool isEdge)
    {
        this.isEdge = isEdge;
    }

    private void Awake()
    {
        sr.sprite = TileSprites.Instance.GetSprites()[spriteNumber];
    }

    public bool GetIsEdge()
    {
        return isEdge;
    }
    
}
