using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent objectParent;
    public KitchenObjectSO GetKitchenSO()
    {
        return kitchenObjectSO;
    }
    public void SetParent(IKitchenObjectParent parent)
    {
        if(this.objectParent != null)
        {
            this.objectParent.ClearKitchenObject();
        }
        this.objectParent = parent;
        parent.SetKitchenObject(this);
        this.transform.parent = parent.getParentTransform();
        transform.localPosition = Vector3.zero;
        if(parent is PlayerInteraction)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            GetComponent<SpriteRenderer>().sortingOrder = -1;
            return;
        }
        GetComponent<SpriteRenderer>().sortingLayerName = "Food";
        GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
    public IKitchenObjectParent GetParent()
    {
        return this.objectParent;
    }
    public void DestroySelf()
    {
        if(objectParent != null)
        {
            objectParent.ClearKitchenObject();
        }
        Destroy(this.gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent)
    {
        KitchenObject newObject = Instantiate(kitchenObjectSO.prefab).GetComponent<KitchenObject>();
        newObject.SetParent(parent);
        return newObject;
    }
}
