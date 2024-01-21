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
    private LayoutGroup layoutGroup;
    [SerializeField] private float spacing;
    private void Awake()
    {
        deliveryManagerUI = this.transform;
        layoutGroup = new LayoutGroup(DeliveryManager.Instance.GetRecipesMax(), spacing, 100);
    }
    private void Start()
    {
        deliveryManager = DeliveryManager.Instance;
        deliveryManager.OnRecipeAdded += DeliveryManager_OnRecipeAdded;
        deliveryManager.OnRecipeRemoved += DeliveryManager_OnRecipeRemoved;
    }

    private void DeliveryManager_OnRecipeRemoved(object sender, DeliveryManager.OnRecipesRemovedEventArgs e)
    {
        List<int> moveIndexs = new List<int>();
        for(int i = 0; i<recipeUIList.Count; i++)
        {
            if (recipeUIList[i].GetRecipeSO() == e.recipeSO)
            {
                recipeUIList[i].RemoveRecipe();
                layoutGroup.SetOccupied(i, out moveIndexs);
                recipeUIList.RemoveAt(i);
                break;
            }
        }
        if (moveIndexs.Count > 0) {
            foreach (int index in moveIndexs)
            {
                LeanTween.moveLocalX(recipeUIList[index].gameObject, layoutGroup.GetPosition(index - 1).x, 0.5f);
            }
        }
        
    }

    private void DeliveryManager_OnRecipeAdded(object sender, DeliveryManager.OnRecipesUpdatedEventArgs e)
    {
        Transform newRecipeUI = Instantiate(recipeContainerPrefab, deliveryManagerUI);
        newRecipeUI.GetComponent<RecipeUI>().CreateRecipeUI(e.recipeSO);
        recipeUIList.Add(newRecipeUI.GetComponent<RecipeUI>());
        newRecipeUI.GetComponent<RectTransform>().anchoredPosition = layoutGroup.GetPosition(recipeUIList.Count - 1);
        layoutGroup.SetOccupied(recipeUIList.Count - 1);
        LeanTween.moveLocalY(newRecipeUI.gameObject, 0, 0.5f).setEase(LeanTweenType.easeInBack);
    }


}
