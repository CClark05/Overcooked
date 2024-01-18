using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class PlateVisual : MonoBehaviour
{
    private FoodRecipeSO[] recipes;
    private PlateKitchenObject plateKitchenObject;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private FoodRecipeSO currentRecipe;
    private List<GameObject> visualPlaceholders = new List<GameObject>();
    private void Awake()
    {
        plateKitchenObject = GetComponent<PlateKitchenObject>();
    }
    private void Start()
    {
        recipes = DeliveryManager.Instance.GetRecipes();
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        bool isValidRecipe = false;
        List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();
        FoodRecipeSO validRecipe = null;
        foreach (KitchenObjectSO kitchenObjectSO in e.kitchenObjectSOList)
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
        }
        
        
        foreach(FoodRecipeSO recipe in recipes)
        {
            var list1 = new HashSet<KitchenObjectSO>(recipe.kitchenObjects);
            var list2 = new HashSet<KitchenObjectSO>(kitchenObjectSOList);
            isValidRecipe = list1.SetEquals(list2);
            if (isValidRecipe)
            {
                validRecipe = recipe;
                break;
            }
        }
        if (isValidRecipe)
        {
            foreach (GameObject obj in visualPlaceholders)
            {
                Destroy(obj);
            }
            visualPlaceholders.Clear();

            spriteRenderer.sprite = validRecipe.sprite;
            currentRecipe = validRecipe;
        }
        else
        {
            if(spriteRenderer.sprite != null)
            {
                spriteRenderer.sprite = null;
            }
            if(visualPlaceholders.Count > 0)
            {
                foreach(GameObject obj in visualPlaceholders)
                {
                    Destroy(obj);
                }
                visualPlaceholders.Clear();
            }
            for(int i = 0; i<kitchenObjectSOList.Count; i++)
            {
                GameObject kitchenObjectVisual = new GameObject(kitchenObjectSOList[i].objectName);
                kitchenObjectVisual.AddComponent<SpriteRenderer>();
                kitchenObjectVisual.GetComponent<SpriteRenderer>().sprite = kitchenObjectSOList[i].prefab.GetComponent<SpriteRenderer>().sprite;
                kitchenObjectVisual.GetComponent<SpriteRenderer>().sortingOrder = kitchenObjectSOList[i].visualOrder + 1;
                kitchenObjectVisual.transform.parent = this.transform;
                kitchenObjectVisual.transform.localPosition = Vector3.zero;
                visualPlaceholders.Add(kitchenObjectVisual);
            }
            
        }
        
    }

 
}
