using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level Data")]
public class LevelData : ScriptableObject
{
    public int duration;
    public int level;
    public FoodRecipeSO[] availableRecipes;
    public int starScore_1;
    public int starScore_2;
    public int starScore_3;
}
