using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : CounterVisual
{
    private PlateCounterInteract plateCounterInteract;
    private List<GameObject> plateVisuals = new List<GameObject>();
    [SerializeField] private Transform plateParent;
    new private void Start()
    {
        base.Start();
        plateCounterInteract = GetComponent<PlateCounterInteract>();
        plateCounterInteract.OnPlateSpawned += PlateCounterInteract_OnPlateSpawned;
        plateCounterInteract.OnPlateRemoved += PlateCounterInteract_OnPlateRemoved;
    }

    private void PlateCounterInteract_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject topPlate = plateVisuals[plateVisuals.Count - 1];
        plateVisuals.Remove(topPlate);
        Destroy(topPlate);
    }

    private void PlateCounterInteract_OnPlateSpawned(object sender, System.EventArgs e)
    {
        
        GameObject newPlate = new GameObject("Plate Visual");
        newPlate.transform.parent = plateParent;
        float plateOffsetY = 0.05f;
        newPlate.transform.localPosition = new Vector3(0, plateOffsetY * plateCounterInteract.GetPlateAmount(), 0);
        newPlate.AddComponent<SpriteRenderer>();
        newPlate.GetComponent<SpriteRenderer>().sprite = GetComponent<PlateCounterInteract>().dirtyPlateSO.prefab.GetComponent<SpriteRenderer>().sprite;
        newPlate.GetComponent<SpriteRenderer>().sortingLayerName = GetComponentInChildren<SpriteRenderer>().sortingLayerName;
        newPlate.GetComponent<SpriteRenderer>().sortingOrder = plateVisuals.Count + 2;
        plateVisuals.Add(newPlate);
    }
}
