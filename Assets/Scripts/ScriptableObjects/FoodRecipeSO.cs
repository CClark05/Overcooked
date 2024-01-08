using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Food Recipe")]
public class FoodRecipeSO : ScriptableObject
{
    public KitchenObjectSO[] kitchenObjects;
    public Sprite sprite;

}
