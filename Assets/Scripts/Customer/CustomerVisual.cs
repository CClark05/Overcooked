using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerVisual : MonoBehaviour
{
    [SerializeField] private Transform foodParent;
    private GameObject foodVisual;
    private void Start()
    {
        GetComponent<CustomerMovement>().OnRecievedFood += GotFood;
        GetComponent<CustomerMovement>().OnDoneEating += DoneEating;
    }
    private void GotFood(FoodRecipeSO recipe)
    {
        foodVisual = new GameObject("food visual");
        foodVisual.transform.parent = foodParent;
        foodVisual.transform.localPosition = Vector3.zero;
        foodVisual.AddComponent<SpriteRenderer>();
        foodVisual.GetComponent<SpriteRenderer>().sprite = recipe.sprite;
    }
    private void DoneEating()
    {
        Debug.Log("test");
        Destroy(foodVisual);
    }
}
