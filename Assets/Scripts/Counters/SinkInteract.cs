using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkInteract : BaseCounter, IHasProgressBar
{
    public Action OnHasDirtyPlate;
    public Action OnSinkEmpty;

    [SerializeField] private List<PlateKitchenObject> dirtyPlates;
    private List<GameObject> cleanPlateVisuals = new List<GameObject>();
    [SerializeField] private KitchenObjectSO cleanPlateSO;
    [SerializeField] private Transform plateParent;
    private float washProgress;
    private float washTime = 4;

    public event EventHandler<IHasProgressBar.OnProgressChangedEventArgs> OnProgressChanged;

    new private void Start()
    {
        base.Start();
    }
    public override void Interact()
    {
        if (player.HasKitchenObject())
        {
            if (PlateKitchenObject.IsPlate(player.GetKitchenObject(), out PlateKitchenObject plate))
            {
                if (plate.IsDirty())
                {
                    dirtyPlates.Add(player.GetKitchenObject() as PlateKitchenObject);
                    player.GetKitchenObject().DestroySelf();
                    OnHasDirtyPlate?.Invoke();
                    return;
                }
                Debug.Log("not dirty");
            }
        }
        else
        {
            if(cleanPlateVisuals.Count > 0)
            {
                KitchenObject.SpawnKitchenObject(cleanPlateSO, player);
                GameObject topPlate = cleanPlateVisuals[cleanPlateVisuals.Count - 1];
                cleanPlateVisuals.Remove(topPlate);
                Destroy(topPlate);
            }
        }
    }
    public override void InteractAlternate()
    {
        if (dirtyPlates.Count == 0) return;
        washProgress++;
        OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
        {
            percentProgress = (float)washProgress / washTime
        });
        if (washProgress >= washTime)
        {
            dirtyPlates.RemoveAt(dirtyPlates.Count - 1);
            AddCleanPlate();
            washProgress = 0;
            if(dirtyPlates.Count == 0)
            {
                OnSinkEmpty?.Invoke();
            }
        }

    }

    public void AddCleanPlate()
    {
        GameObject newPlate = new GameObject("Plate Visual");
        newPlate.transform.parent = plateParent;
        float plateOffsetY = 0.05f;
        newPlate.transform.localPosition = new Vector3(0, plateOffsetY * cleanPlateVisuals.Count, 0);
        newPlate.AddComponent<SpriteRenderer>();
        newPlate.GetComponent<SpriteRenderer>().sprite = cleanPlateSO.prefab.GetComponent<SpriteRenderer>().sprite;
        newPlate.GetComponent<SpriteRenderer>().sortingOrder = cleanPlateVisuals.Count;
        cleanPlateVisuals.Add(newPlate);
    }

}
