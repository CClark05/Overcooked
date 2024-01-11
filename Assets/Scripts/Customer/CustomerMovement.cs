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
        RecievedFood,
        EatingFood,
        LeavingStore
    }
    [SerializeField] private States state;
    private CustomerData customerData;
    public Action<FoodRecipeSO> OnRecievedFood;
    public Action OnDoneEating;
    private PathfindingMovement pathfindingMovement;
    private FoodRecipeSO recipe;

    private void Awake()
    {
        customerData = GetComponent<CustomerData>();
        pathfindingMovement = GetComponent<PathfindingMovement>();
        state = States.WaitingForFood;
    }
    private void Start()
    {
        customerData.OnFoodReady += FoodReady;
    }
    private void Update()
    {
        switch (state)
        {
            case States.WaitingForFood:

                break;
            case States.GettingFood:
                //Debug.Log("Getting food");
                pathfindingMovement.SetTarget(Target.TargetNames.CustomerPickup, () => {
                    state = States.RecievedFood;
                    OnRecievedFood?.Invoke(recipe);
                });
                break;
            case States.RecievedFood:
                //Debug.Log("Recieved food");
                
                pathfindingMovement.SetTarget(Target.TargetNames.EatFood, () =>
                {
                    state = States.EatingFood;
                    FunctionTimer.Create(() =>
                    {
                        state = States.LeavingStore;
                        OnDoneEating?.Invoke();

                    }, customerData.GetEatTime());
                });
                break;
            case States.EatingFood:
                //Debug.Log("Eating Food");
                break;
            case States.LeavingStore:
                //Debug.Log("left store");
                
                pathfindingMovement.SetTarget(Target.TargetNames.ExitStore, ()=> Destroy(this.gameObject));
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
}
