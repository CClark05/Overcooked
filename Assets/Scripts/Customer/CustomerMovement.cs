using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public enum States
    {
        WaitingForFood,
        RecievedFood,
        EatingFood,
        LeavingStore
    }
    [SerializeField] private States state;
    private CustomerData customerData;
    private void Awake()
    {
        customerData = GetComponent<CustomerData>();
        state = States.WaitingForFood;
    }
    private void Start()
    {
        customerData.OnGotFood += GotFood;
    }
    
    public void GotFood(FoodRecipeSO recipe)
    {
        if(recipe == null)
        {
            state = States.LeavingStore;
        }
        state = States.RecievedFood;
    }

    public void SetState(States state)
    {
        this.state = state;
    }
}
