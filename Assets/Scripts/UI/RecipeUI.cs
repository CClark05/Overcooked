using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private Transform ingredientUIPrefab;
    [SerializeField] private Transform ingredientsContainer;
    [SerializeField] private Image foodImage;
    [SerializeField] private Image background;
    [SerializeField] private Image progressBar;
    private List<Image> ingredientImages = new List<Image>();
    private List<Image> foodImages = new List<Image>();
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
            ingredientImages.Add(newIngredintUI.GetComponent<Image>());
            foodImages.Add(newIngredintUI.GetChild(0).GetComponent<Image>());
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
    public void RemoveRecipe()
    {
        float fadeTime = 0.8f;
        LeanTween.value(this.gameObject, ValueCallback, 1, 0, fadeTime).setOnComplete(() => Destroy(this.gameObject));
        void ValueCallback(float val)
        {
            foodImage.color = new Color(1, 1, 1, val);
            background.color = new Color(1, 1, 1, val);
            foreach (Image image in ingredientImages)
            {
                image.color = new Color(1, 1, 1, val);
            }
            foreach (Image image in foodImages)
            {
                image.color = new Color(1, 1, 1, val);
            }
        }
    }

    

}
