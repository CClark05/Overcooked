using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cooking Recipe")]
public class CookingRecipeSO : ScriptableObject
{
    public KitchenObjectSO raw;
    public KitchenObjectSO cooked;
    public KitchenObjectSO burned;
    public float cookTime;
    public float burnTime;
    
}