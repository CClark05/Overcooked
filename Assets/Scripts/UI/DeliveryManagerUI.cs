using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUI : MonoBehaviour
{
    private DeliveryManager deliveryManager;
    [SerializeField] private Transform recipeContainerPrefab;
    private Transform deliveryManagerUI;
    private List<RecipeUI> recipeUIList = new List<RecipeUI>();
    private void Awake()
    {
        deliveryManagerUI = this.transform;
    }
    private void Start()
    {
        deliveryManager = DeliveryManager.Instance;
        deliveryManager.OnRecipeAdded += DeliveryManager_OnRecipeAdded;
        deliveryManager.OnRecipeRemoved += DeliveryManager_OnRecipeRemoved;
    }

    private void DeliveryManager_OnRecipeRemoved(object sender, DeliveryManager.OnRecipesRemovedEventArgs e)
    {
        for(int i = 0; i<recipeUIList.Count; i++)
        {
            if (recipeUIList[i].GetRecipeSO() == e.recipeSO)
            {
                Destroy(recipeUIList[i].gameObject);
                recipeUIList.RemoveAt(i);
                break;
            }
        }
    }

    private void DeliveryManager_OnRecipeAdded(object sender, DeliveryManager.OnRecipesUpdatedEventArgs e)
    {
        Transform newRecipeUI = Instantiate(recipeContainerPrefab, deliveryManagerUI);
        newRecipeUI.GetComponent<RecipeUI>().CreateRecipeUI(e.recipeSO);
        recipeUIList.Add(newRecipeUI.GetComponent<RecipeUI>());
    }


}
