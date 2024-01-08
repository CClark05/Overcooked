using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs: EventArgs
    {
        public List<KitchenObjectSO> kitchenObjectSOList;
    }
    private List<KitchenObjectSO> ingredientList;
    [SerializeField] private bool isDirty;
    private void Awake()
    {
        ingredientList = new List<KitchenObjectSO>();
    }
    public List<KitchenObjectSO> GetIngredientList()
    {
        return ingredientList;
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (isDirty) return false;
        if (ingredientList.Contains(kitchenObjectSO) || !kitchenObjectSO.isPreppedIngredient)
        {
            return false;
        }
        ingredientList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObjectSOList = ingredientList
        }); 
        return true;
    }
    public static bool IsPlate(KitchenObject kitchenObject, out PlateKitchenObject plateKitchenObject)
    {
        if(kitchenObject is PlateKitchenObject)
        {
            plateKitchenObject = kitchenObject as PlateKitchenObject;
            return true;
        }
        plateKitchenObject = null;
        return false;
    }
    public bool IsDirty()
    {
        return isDirty;
    }
}
