using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashBinAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    private float animationTime = 0.5f;

    private void Start()
    {
        GetComponent<TrashBinInteract>().OnDestroyedObject += kitchenObject =>
        {
            if (PlateKitchenObject.IsPlate(kitchenObject, out PlateKitchenObject plate))
            {
                PlateVisual plateVisual = plate.gameObject.GetComponent<PlateVisual>();
                if (plateVisual.visualPlaceholders.Count == 0)
                {
                    GameObject newFood = CreateSprite(plateVisual.FoodSprite);
                    FoodAnimation(newFood);
                    return;
                }
                foreach (GameObject ingredient in plateVisual.visualPlaceholders)
                {
                    GameObject newIngredient = CreateSprite(ingredient.GetComponent<SpriteRenderer>().sprite);
                    FoodAnimation(newIngredient);
                }
                return;
            }
            GameObject newKitchenObject = Instantiate(kitchenObject.GetKitchenSO().prefab.gameObject, transform, true);
            newKitchenObject.transform.localPosition = Vector3.zero;
            FoodAnimation(newKitchenObject);
        };
    }
    private void FoodAnimation(GameObject obj)
    {
        LeanTween.scale(obj, Vector3.zero, animationTime).setOnComplete(() => Destroy(obj));
    }

    private GameObject CreateSprite(Sprite sprite)
    {
        GameObject newFood = new GameObject("food");
        newFood.AddComponent<SpriteRenderer>().sprite = sprite;
        newFood.transform.parent = this.transform;
        newFood.transform.localPosition = Vector3.zero;
        newFood.GetComponent<SpriteRenderer>().sortingLayerName = sr.sortingLayerName;
        newFood.GetComponent<SpriteRenderer>().sortingOrder = 1;
        return newFood;
    }
}