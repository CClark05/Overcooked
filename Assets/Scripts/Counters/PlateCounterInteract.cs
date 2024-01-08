using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterInteract : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    public KitchenObjectSO dirtyPlateSO { get; private set; }
    [SerializeField] private KitchenObjectSO _dirtyPlateSO;

    private int plateAmount;
    new private void Awake()
    {
        base.Awake();
        dirtyPlateSO = _dirtyPlateSO;
    }
    new private void Start()
    {
        base.Start();
        DeliveryCounterInteract.OnFoodDelivered += () =>
        {
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            plateAmount++;
        };
    }
    /**
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnPlateTimer)
        {
            if (plateAmount < maxPlates)
            {
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                plateAmount++;
            }
            timer = 0;
        }
    }
    */
    public override void Interact()
    {
        if (!player.HasKitchenObject())
        {
            if(plateAmount > 0)
            {
                plateAmount--;
                KitchenObject.SpawnKitchenObject(dirtyPlateSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public int GetPlateAmount()
    {
        return plateAmount;
    }

}
