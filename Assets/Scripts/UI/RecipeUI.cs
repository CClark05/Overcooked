using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private Transform ingredientUIPrefab;
    [SerializeField] private Transform ingredientsContainer;
    [SerializeField] private Image foodImage;
    [SerializeField] private Image progressBar;
    private CustomerData customer;
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
    private void Start()
    {
        customer = CustomerSpawner.Instance.GetCustomer(recipeSO);
    }
    private void Update()
    {
        UpdateProgress(customer.GetPatiencePercentage());
    }
    public FoodRecipeSO GetRecipeSO()
    {
        return recipeSO;
    }

    private void UpdateProgress(float percent)
    {
        progressBar.fillAmount = percent;
    }
    
    public void SetCustomer(CustomerData customer)
    {
        this.customer = customer;
    }


}
