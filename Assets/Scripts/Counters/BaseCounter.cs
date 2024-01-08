using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    
    protected KitchenObject currentKitchenObject;
    protected PlayerInteraction player;
    [SerializeField] private KitchenObjectSO addKitchenObject;
    public void Awake()
    {
        TileManager.Instance.AddTile(transform.position, this);
    }
    public void Start()
    {
        player = PlayerInteraction.Instance;
        if(addKitchenObject != null)
        {
            KitchenObject.SpawnKitchenObject(addKitchenObject, this);
            GetKitchenObject().transform.localPosition = GetComponent<CounterVisual>().GetFodVisual().localPosition;
        }
    }

    public virtual void Interact()
    {
        Debug.LogError("BaseCounter.Interact()");
    }
    public virtual void InteractAlternate()
    {
    }


    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        currentKitchenObject = kitchenObject;
    }
    public Transform getParentTransform()
    {
        return this.transform;
    }
    public void ClearKitchenObject()
    {
        currentKitchenObject = null;
    }
    public KitchenObject GetKitchenObject()
    {
        return currentKitchenObject;
    }
    public bool HasKitchenObject()
    {
        return currentKitchenObject != null;
    }
}
