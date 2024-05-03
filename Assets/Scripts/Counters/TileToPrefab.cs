
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
    [SerializeField] private bool addShadow;
    
    public void SpriteNumber(int num) => spriteNumber = num;
    public void IsEdge(bool isEdge) => this.isEdge = isEdge;

    public void FoodXPos(float xPos) => foodXPos = xPos;

    private void Awake() => sr.sprite = TileSprites.Instance.GetSprites()[spriteNumber];

    public bool GetIsEdge() => isEdge;

    public float GetFoodXPos() => foodXPos;
    public bool GetAddShaodw() => addShadow;

    public void AddShadow(bool addShadow) => this.addShadow = addShadow;
}
