using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent 
{
    public void SetKitchenObject(KitchenObject kitchenObject);
    public Transform getParentTransform();
    public void ClearKitchenObject();
    public KitchenObject GetKitchenObject();
    public bool HasKitchenObject();
}
