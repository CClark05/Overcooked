using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorVisual : CounterVisual
{
    [SerializeField] private Sprite floorShadowSprite;
    [SerializeField] private bool isShadow;
    new public void Start()
    {
        base.Start();
        GetComponent<FloorInteract>().OnItemPickup += () =>
        {
            sr.material = MaterialsManager.Instance.defaultMaterial;
        };
        if (isShadow) sr.sprite = floorShadowSprite;
    }
    public override void Instance_onSelectedCounterChanged(object sender, PlayerInteraction.onSelectedCounterChangedEventArgs e)
    {
        if (GetComponent<FloorInteract>().HasKitchenObject())
        {
            if (e.selectedCounter == counterInteract)
            {
                sr.material = MaterialsManager.Instance.selectedMaterial;
            }
            else
            {
                sr.material = MaterialsManager.Instance.defaultMaterial;
            }
        }
    }
    public void IsShadow(bool isShadow)
    {
        this.isShadow = isShadow;
    }
}
