using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public static List<Chair> chairs = new List<Chair>();
    [SerializeField] private CustomerMovement customer;
    private void Awake()
    {
        chairs.Add(this);
    }
    public bool HasCustomer()
    {
        return customer != null;
    }
    public static Chair FindEmptyChair()
    {
        Chair randomChair;
        do
        {
            randomChair = chairs[UnityEngine.Random.Range(0, chairs.Count)];
        } while (randomChair.HasCustomer());

        if(randomChair != null) return randomChair;

        Debug.LogError("No empty chairs");
        return null;
    }
    public void AddCustomer(CustomerMovement customer)
    {
        this.customer = customer;
    }
    public void RemoveCustomer()
    {
        customer = null;
    }
}
