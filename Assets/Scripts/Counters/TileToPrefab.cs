
using SuperTiled2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileToPrefab : MonoBehaviour
{

    public int spriteNumber;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private bool isEdge;
    [SerializeField] private float foodXPos;
    public void SpriteNumber(int num)
    {
        spriteNumber = num;
    }
    public void IsEdge(bool isEdge)
    {
        this.isEdge = isEdge;
    }

    public void FoodXPos(float xPos)
    {
        foodXPos = xPos;
    }
    
    private void Awake()
    {
        sr.sprite = TileSprites.Instance.GetSprites()[spriteNumber];
    }

    public bool GetIsEdge()
    {
        return isEdge;
    }

    public float GetFoodXPos()
    {
        return foodXPos;
    }

}
