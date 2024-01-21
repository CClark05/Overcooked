using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCounterInteract : BaseCounter
{
    public override void Interact()
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetParent(this);
                GetKitchenObject().transform.localPosition = GetComponent<CounterVisual>().GetFodVisual().localPosition;
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetParent(player);
            }
            else
            {
                if(PlateKitchenObject.IsPlate(player.GetKitchenObject(), out PlateKitchenObject plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    
                }
                if(PlateKitchenObject.IsPlate(GetKitchenObject(), out plate))
                {
                    if (plate.TryAddIngredient(player.GetKitchenObject().GetKitchenSO()))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }
                    if(PlateKitchenObject.IsPlate(player.GetKitchenObject(), out PlateKitchenObject playerPlate))
                    {
                        List<KitchenObjectSO> kitchenObjects = playerPlate.GetIngredientList();
                        if (kitchenObjects.Count == 0) return;
                        if (plate.TryAddIngredients(kitchenObjects))
                        {
                            playerPlate.ClearPlate();
                        }

                    }
                }
            }
        }
    }

}
