using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerVisual : CounterVisual
{
    [SerializeField] private SpriteRenderer ingredientVisual;
    private ContainerInteract container;
    private Sprite ingredientSprite;
    new private void Awake()
    {
        base.Awake();
        container = counterInteract as ContainerInteract;
        ingredientSprite = container.GetKitchenObjectSO().prefab.GetComponent<SpriteRenderer>().sprite;
        ingredientVisual.sprite = ingredientSprite;
    }

}
