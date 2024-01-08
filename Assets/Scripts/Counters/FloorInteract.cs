using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorInteract : BaseCounter
{
    [SerializeField] private const int interactLayer = 6;
    private LayerMask defaultLayer;
    public Action OnItemPickup;
    private List<KitchenObject> previousKitchenObjects = new List<KitchenObject>();
    new private void Awake()
    {
        base.Awake();
        defaultLayer = gameObject.layer;
    }
    public override void Interact()
    {
        if (HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetParent(player);
                OnItemPickup?.Invoke();
                if(previousKitchenObjects.Count > 0)
                {
                    SetKitchenObject(previousKitchenObjects[previousKitchenObjects.Count - 1]);
                    previousKitchenObjects.RemoveAt(previousKitchenObjects.Count - 1);
                    return;
                }
                gameObject.layer = defaultLayer;
            }
        }
    }
    public void HasObject()
    {
        gameObject.layer = interactLayer;
    }
    public void AddToList(KitchenObject kitchenObject)
    {
        previousKitchenObjects.Add(kitchenObject);
    }
}
