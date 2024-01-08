using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private Transform ingredientUIPrefab;
    [SerializeField] private Transform ingredientsContainer;
    [SerializeField] private Image foodImage;
    private FoodRecipeSO recipeSO;
    public void CreateRecipeUI(FoodRecipeSO recipe)
    {
        foodImage.sprite = recipe.sprite;
        int numIngredients = recipe.kitchenObjects.Length;
        for(int i =0; i<numIngredients; i++)
        {
            Transform newIngredintUI = Instantiate(ingredientUIPrefab, ingredientsContainer);
            newIngredintUI.GetChild(0).GetComponent<Image>().sprite = recipe.kitchenObjects[i].prefab.GetComponent<SpriteRenderer>().sprite;
            recipeSO = recipe;
        }
    }

    public FoodRecipeSO GetRecipeSO()
    {
        return recipeSO;
    }
            


}
