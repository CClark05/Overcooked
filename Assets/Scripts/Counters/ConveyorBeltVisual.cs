using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltVisual : CounterVisual
{
    [SerializeField] private Sprite[] arrowSprites;
    private SpriteRenderer arrowVisual;
    private ConveyorBeltInteract belt;

    new private void Awake()
    {
        base.Awake();
        belt = GetComponent<ConveyorBeltInteract>();
        arrowVisual = foodVisual.GetComponent<SpriteRenderer>();
        belt.OnHasDirection += (ConveyorBeltInteract.Directions direction) =>
        {
            switch (direction)
            {
                case ConveyorBeltInteract.Directions.Up:
                    arrowVisual.sprite = arrowSprites[0];
                    break;
                case ConveyorBeltInteract.Directions.Down:
                    arrowVisual.sprite = arrowSprites[1];
                    break;
                case ConveyorBeltInteract.Directions.Left:
                    arrowVisual.sprite = arrowSprites[2];
                    break;
                case ConveyorBeltInteract.Directions.Right:
                    arrowVisual.sprite = arrowSprites[3];
                    break;
            }
        };
    }

    new private void Start()
    {
        foodYPos = 0.07f;
        base.Start();
    }


}
