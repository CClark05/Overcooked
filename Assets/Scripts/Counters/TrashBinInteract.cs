using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinInteract : BaseCounter
{
    public Action<KitchenObject> OnDestroyedObject;
    public override void Interact()
    {
        if (player.HasKitchenObject())
        {
            OnDestroyedObject?.Invoke(player.GetKitchenObject());
            if (PlateKitchenObject.IsPlate(player.GetKitchenObject(), out PlateKitchenObject plate))
            {
                player.GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(plate.GetKitchenSO(), player);
                return;
            }
            player.GetKitchenObject().DestroySelf();
        }
    }
}
