using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CuttingBoardInteract : BaseCounter, IHasProgressBar, IParticles
{
    public event EventHandler<IHasProgressBar.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private CuttingRecipeSO[] recipeArray;
    private float cutProgress;
    public event Action OnPlayParticles;

    public override void Interact()
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipe(player.GetKitchenObject().GetKitchenSO()))
            {
                cutProgress = 0;
                OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                {
                    percentProgress = 0
                }); 
                player.GetKitchenObject().SetParent(this);
                GetKitchenObject().transform.localPosition = GetComponent<CounterVisual>().GetFodVisual().localPosition;
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                {
                    percentProgress = 0
                });
                GetKitchenObject().SetParent(player);
            }
            else
            {
                if (PlateKitchenObject.IsPlate(player.GetKitchenObject(), out PlateKitchenObject plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
            }
        }
    }
    public override void InteractAlternate()
    {
        if (HasKitchenObject())
        {
            CuttingRecipeSO recipe = FindRecipe(GetKitchenObject().GetKitchenSO());
            if (recipe != null)
            {
                cutProgress++;
                OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                {
                    percentProgress = (float)cutProgress / recipe.cutTime
                });
                if (cutProgress >= recipe.cutTime)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(recipe.output, this);
                    GetKitchenObject().transform.localPosition = GetComponent<CounterVisual>().GetFodVisual().localPosition;
                    OnPlayParticles?.Invoke();
                }
            }
        }
    }
    private bool HasRecipe(KitchenObjectSO input)
    {
        if(FindRecipe(input) != null)
        {
            return true;
        }
        return false;
    }
    private CuttingRecipeSO FindRecipe(KitchenObjectSO input)
    {
        foreach(CuttingRecipeSO recipe in recipeArray)
        {
            if(recipe.input == input)
            {
                return recipe;
            }
        }
        return null;
    }

    
}
