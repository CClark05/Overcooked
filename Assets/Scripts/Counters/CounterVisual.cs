using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterVisual : MonoBehaviour
{
    public Action OnIsEdge;
    protected SpriteRenderer sr;
    protected BaseCounter counterInteract;
    [SerializeField] protected Transform foodVisual;
    protected float foodYPos = 0.1275f;
    protected void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        counterInteract = GetComponent<BaseCounter>();
        
    }
    protected void Start()
    {
        PlayerInteraction.Instance.OnSelectedCounterChanged += Instance_onSelectedCounterChanged;
        if (TryGetComponent(out TileToPrefab tileToPrefab))
        {
            if (tileToPrefab.GetIsEdge())
            {
                foodVisual.localPosition = new Vector3(0, foodYPos, 0);
                return;
            }
            foodVisual.localPosition = new Vector3(0, 0, 0);
        }
    }

    public virtual void Instance_onSelectedCounterChanged(object sender, PlayerInteraction.onSelectedCounterChangedEventArgs e)
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
   
    public Transform GetFodVisual()
    {
        return foodVisual;
    }

}
