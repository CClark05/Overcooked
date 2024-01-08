using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ConveyorBeltInteract : BaseCounter
{
    public Action<Directions> OnHasDirection;
    public enum Directions
    {
        Up, Down, Left, Right
    }
    [SerializeField] private Directions direction;
    [SerializeField] private bool isStartingPoint;
    private List<Waypoint> path = new List<Waypoint>();

    private float conveyorSpeed = 0.8f;

    private Vector2 directionVector;
    private Vector2 nextTile;
    private BaseCounter nextCounter;
    private bool hasNextCounter;

    new private void Start()
    {
        base.Start();
        switch (direction)
        {
            case Directions.Up:
                nextTile = (Vector2)transform.position + new Vector2(0, 1);
                directionVector = Vector2.up;
                OnHasDirection?.Invoke(Directions.Up);
                break;
            case Directions.Down:
                nextTile = (Vector2)transform.position + new Vector2(0, -1);
                directionVector = Vector2.down;
                OnHasDirection?.Invoke(Directions.Down);
                break;
            case Directions.Left:
                nextTile = (Vector2)transform.position + new Vector2(-1, 0);
                directionVector = Vector2.left;
                OnHasDirection?.Invoke(Directions.Left);
                break;
            case Directions.Right:
                nextTile = (Vector2)transform.position + new Vector2(1, 0);
                directionVector = Vector2.right;
                OnHasDirection?.Invoke(Directions.Right);
                break;
        }
        hasNextCounter = TileManager.Instance.TryGetBaseCounterFromPosition(nextTile, out nextCounter);
        if(!(nextCounter is ConveyorBeltInteract) || nextCounter == null)
        {
            ConveyorBeltManager.Instance.OnIsFinalBelt?.Invoke((Vector2)transform.position + directionVector / 2, directionVector);
        }

        ConveyorBeltManager.Instance.OnPathfinderDone += () =>
        {
            path = ConveyorBeltManager.Instance.GetPathFromPosition(transform.position);
        };
        
        
    }
    

    public override void Interact()
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetParent(this);
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetParent(player);
            }
            else
            {
                if (PlateKitchenObject.IsPlate(player.GetKitchenObject(), out PlateKitchenObject plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
                if (PlateKitchenObject.IsPlate(GetKitchenObject(), out plate))
                {
                    if (plate.TryAddIngredient(player.GetKitchenObject().GetKitchenSO()))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    public bool IsStartingPoint()
    {
        return isStartingPoint;
    }

    private void Update()
    {
        if (HasKitchenObject() && hasNextCounter)
        {
            if (nextCounter.HasKitchenObject())
            {
                return;
            }
            Vector2 destination = path[1].GetPosition();
            GetKitchenObject().transform.position = Vector2.MoveTowards(GetKitchenObject().transform.position, destination, conveyorSpeed * Time.deltaTime);
            if(Vector2.Distance(GetKitchenObject().transform.position, destination) < 0.01f)
            {
                GetKitchenObject().SetParent(nextCounter);
                
            }
        }
    }

    public Vector2 GetDirectionVector()
    {
        return directionVector;
    }
    public Directions GetDirection()
    {
        return direction;
    }

}
