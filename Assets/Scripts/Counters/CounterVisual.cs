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
    protected float foodYPos = 0.125f;
    protected Color originalColor;
    protected string selectedColor = "#CCCCCC";
    protected void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        counterInteract = GetComponent<BaseCounter>();
        originalColor = sr.color;
    }
    protected void Start()
    {
        PlayerInteraction.Instance.OnSelectedCounterChanged += Instance_onSelectedCounterChanged;
        if (TryGetComponent(out TileToPrefab tileToPrefab))
        {
            if (tileToPrefab.GetIsEdge())
            {
                foodVisual.localPosition = new Vector3(0, foodYPos, 0);
            }

            if (tileToPrefab.GetFoodXPos() != 0)
            {
                foodVisual.localPosition = new Vector3(tileToPrefab.GetFoodXPos(), foodVisual.localPosition.y, 0);
            }
        }
    }

    public virtual void Instance_onSelectedCounterChanged(object sender, PlayerInteraction.onSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == counterInteract)
        {
            ColorUtility.TryParseHtmlString(selectedColor, out Color newColor);
            sr.color = newColor;
            //sr.material = MaterialsManager.Instance.selectedMaterial;
        }
        else
        {
            sr.color = originalColor;
            //sr.material = MaterialsManager.Instance.defaultMaterial;
        }
    }
   
    public Transform GetFodVisual()
    {
        return foodVisual;
    }

}
