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
    public Action<FoodRecipeSO> OnRecipeDelivered;
    public class OnRecipesRemovedEventArgs : EventArgs
    {
        public FoodRecipeSO recipeSO;
    }
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private float spawnRecipeTimer;
    [SerializeField] private FoodRecipeSO[] availableRecipes;
    private List<FoodRecipeSO> currentRecipes = new List<FoodRecipeSO>();
    private int amountOfRecipesMax = 4;
    private int amountOfRecipesDelivered;
    private int amountOfRecipesFailed;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CustomerData.OnLostPatience += CustomerLeftStore;
        GameManager.Instance.OnGameStarted += SpawnRecipe;
    }
    private float timer = -0.5f;
    private void Update()
    {
        if (GameManager.Instance.GetState() == GameManager.States.Playing)
        {
                timer += Time.deltaTime;
                if (timer >= spawnRecipeTimer)
                {
                    if (currentRecipes.Count < amountOfRecipesMax)
                    {
                        SpawnRecipe();
                    }
                    timer = 0;
                }
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
                OnRecipeDelivered?.Invoke(recipe);
                amountOfRecipesDelivered++;
                return true;
            }
        }
        return false;
    }
    private void CustomerLeftStore(FoodRecipeSO recipe)
    {
        foreach(FoodRecipeSO currentRecipe in currentRecipes)
        {
            if(currentRecipe == recipe)
            {
                currentRecipes.Remove(recipe);
                OnRecipeRemoved?.Invoke(this, new OnRecipesRemovedEventArgs
                {
                    recipeSO = recipe
                });
                amountOfRecipesFailed++;
                timer = 0;
                return;
            }
        }
        Debug.LogError("Customer had invalid recipe");
    }
    private void SpawnRecipe()
    {
        FoodRecipeSO randomRecipe = availableRecipes[UnityEngine.Random.Range(0, availableRecipes.Length)];
        currentRecipes.Add(randomRecipe);
        OnRecipeAdded?.Invoke(this, new OnRecipesUpdatedEventArgs
        {
            recipeSO = randomRecipe
        });
    }
    public FoodRecipeSO[] GetRecipes()
    {
        return availableRecipes;
    }
    public List<FoodRecipeSO> GetCurrentRecipes()
    {
        return currentRecipes;
    }

    public int GetAmountOfRecipesDelivered()
    {
        return amountOfRecipesDelivered;
    }

    public int GetRecipesMax()
    {
        return amountOfRecipesMax;
    }
}
