using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler<OnRecipesUpdatedEventArgs> OnRecipeAdded;
    public class OnRecipesUpdatedEventArgs : EventArgs
    {
        public FoodRecipeSO recipeSO;
    }
    public event EventHandler<OnRecipesRemovedEventArgs> OnRecipeRemoved;
    public class OnRecipesRemovedEventArgs : EventArgs
    {
        public FoodRecipeSO recipeSO;
    }
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private float spawnRecipeTimer;
    [SerializeField] private FoodRecipeSO[] recipes;
    private List<FoodRecipeSO> currentRecipes = new List<FoodRecipeSO>();
    private int amountOfRecipesMax = 4;
    private int amountOfRecipesDelivered = 0;
    private void Awake()
    {
        Instance = this;
    }

    private float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRecipeTimer)
        {
            if (currentRecipes.Count < amountOfRecipesMax)
            {
                FoodRecipeSO randomRecipe = recipes[UnityEngine.Random.Range(0, recipes.Length)];
                currentRecipes.Add(randomRecipe);
                OnRecipeAdded?.Invoke(this, new OnRecipesUpdatedEventArgs
                {
                    recipeSO = randomRecipe
                });
            }
            timer = 0;
        }
    }
    public bool DeliverRecipe(PlateKitchenObject plate)
    {
        foreach (FoodRecipeSO recipe in currentRecipes)
        {
            var list1 = new HashSet<KitchenObjectSO>(recipe.kitchenObjects);
            var list2 = new HashSet<KitchenObjectSO>(plate.GetIngredientList());
            bool isValidRecipe = list1.SetEquals(list2);
            if (isValidRecipe)
            {
                currentRecipes.Remove(recipe);
                OnRecipeRemoved?.Invoke(this, new OnRecipesRemovedEventArgs
                {
                    recipeSO = recipe
                });
                amountOfRecipesDelivered++;
                return true;
            }
        }
        return false;
    }
    public FoodRecipeSO[] GetRecipes()
    {
        return recipes;
    }
    public List<FoodRecipeSO> GetCurrentRecipes()
    {
        return currentRecipes;
    }

    public int GetAmountOfRecipesDelivered()
    {
        return amountOfRecipesDelivered;
    }
}
