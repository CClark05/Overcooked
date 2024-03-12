using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner Instance;

    [SerializeField] private GameObject customerPrefab;
    private List<CustomerData> customers = new List<CustomerData>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<CustomerData.Moods> customerMoods = new List<CustomerData.Moods>();
    public Action<CustomerData.Moods> OnMoodAdded;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeAdded += Instance_OnRecipeAdded;
        DeliveryManager.Instance.OnRecipeDelivered += RecipeDelivered;

        CustomerData.OnLostPatience += (FoodRecipeSO recipe) =>
        {
            foreach (CustomerData customer in customers)
            {
                if (customer.GetRecipe() == recipe)
                {
                    customers.Remove(customer);
                    return;
                }
            }
        };
    }

    private void Instance_OnRecipeAdded(object sender, DeliveryManager.OnRecipesUpdatedEventArgs e)
    {
        SpawnCustomer(e.recipeSO);
    }

    private void SpawnCustomer(FoodRecipeSO recipe)
    {
        GameObject newCustomer = Instantiate(customerPrefab);
        newCustomer.GetComponent<CustomerData>().SetRecipe(recipe);
        newCustomer.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
        customers.Add(newCustomer.GetComponent<CustomerData>());
        
    }

    private void RecipeDelivered(FoodRecipeSO recipe)
    {
        foreach(CustomerData customer in customers)
        {
            if(customer.GetRecipe() == recipe)
            {
                customerMoods.Add(customer.GetMood());
                customer.FoodReady();
                OnMoodAdded?.Invoke(customer.GetMood());
                return;
            }
        }
    }

    public CustomerData GetCustomer(FoodRecipeSO recipe)
    {
        foreach (CustomerData customer in customers)
        {
            if (customer.GetRecipe() == recipe && !customer.hasUI)
            {
                customer.hasUI = true;
                return customer;
            }
        }
        Debug.LogError("recipe not valid");
        return null;
    }
    public List<CustomerData.Moods> GetCustomerMoods()
    {
        return customerMoods;
    }
}
