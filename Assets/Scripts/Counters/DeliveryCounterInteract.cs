using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounterInteract : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateObjectSO;
    public static Action OnFoodDelivered;
    public override void Interact()
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().GetKitchenSO() == plateObjectSO)
            {
                if (DeliveryManager.Instance.DeliverRecipe(player.GetKitchenObject() as PlateKitchenObject))
                {
                    player.GetKitchenObject().DestroySelf();
                    OnFoodDelivered?.Invoke();
                }
            }

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            OnFoodDelivered?.Invoke();
        }
    }
}
