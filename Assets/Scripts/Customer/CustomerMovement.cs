using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public enum States
    {
        WaitingForFood,
        GettingFood,
        LeavingStore
    }
    [SerializeField] private States state;
    private float moveDistance = 2;
    private float idleTime = 3;
    private CustomerData customerData;
    public Action<FoodRecipeSO> OnRecievedFood;
    public Action OnDoneEating;

    private PathfindingMovement pathfindingMovement;
    private FoodRecipeSO recipe;
    Target randomTarget = null;
    public Vector2 MovementDirection => pathfindingMovement.CurrentMovementDirection;
    
    private Vector2 lastUpdatedDirection;
    public Vector2 LastUpdatedDirection
    {
        get
        {
            if (MovementDirection != Vector2.zero)
            {
                lastUpdatedDirection = MovementDirection;
            }

            return lastUpdatedDirection;
        }
    }
    private void Awake()
    {
        customerData = GetComponent<CustomerData>();
        pathfindingMovement = GetComponent<PathfindingMovement>();
        state = States.WaitingForFood;
        
    }
    private void Start()
    {
        GameManager.Instance.OnGameEnded += () =>
        {
            pathfindingMovement.RemoveTarget();
        };
        customerData.OnFoodReady += FoodReady;     
    }
    private void Update()
    {
        switch (state)
        {
            case States.WaitingForFood:
                
                if (randomTarget == null)
                {
                    randomTarget = PathfindingTarget.GetRandomTarget(moveDistance, transform.position);
                    pathfindingMovement.SetTarget(randomTarget, () =>
                    {
                        FunctionTimer.Create(() => randomTarget = null, UnityEngine.Random.Range(idleTime, idleTime + 2));
                    });
                }
                
                break;
            case States.GettingFood:
                //Debug.Log("Getting food");
                pathfindingMovement.SetTarget(Target.TargetNames.CustomerPickup, () => {
                    state = States.LeavingStore;
                    OnRecievedFood?.Invoke(recipe);
                });
                break;
            case States.LeavingStore:
                //Debug.Log("left store");
                
                pathfindingMovement.SetTarget(Target.TargetNames.ExitStore, LeaveStore);
                break;
        }
    }
    public void FoodReady(FoodRecipeSO recipe)
    {
        if(recipe == null)
        {
            state = States.LeavingStore;
            return;
        }
        state = States.GettingFood;
        this.recipe = recipe;
    }

    public void SetState(States state)
    {
        this.state = state;
    }

    private void LeaveStore()
    {
        Destroy(this.gameObject);
    }
}
