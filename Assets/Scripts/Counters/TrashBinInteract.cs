using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinInteract : BaseCounter
{
    public override void Interact()
    {
        if (player.HasKitchenObject())
        {
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
