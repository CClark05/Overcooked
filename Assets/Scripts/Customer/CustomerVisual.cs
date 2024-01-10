using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerVisual : MonoBehaviour
{
    [SerializeField] private Transform foodParent;
    private void Start()
    {
        GetComponent<CustomerData>().OnGotFood += GotFood;
    }
    private void GotFood(FoodRecipeSO recipe)
    {
        GameObject foodVisual = new GameObject("food visual");
        foodVisual.transform.parent = foodParent;
        foodVisual.transform.localPosition = Vector3.zero;
        foodVisual.AddComponent<SpriteRenderer>();
        foodVisual.GetComponent<SpriteRenderer>().sprite = recipe.sprite;
    }
}
