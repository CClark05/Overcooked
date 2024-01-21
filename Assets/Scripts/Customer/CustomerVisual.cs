using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerVisual : MonoBehaviour
{
    [SerializeField] private Transform foodParent;
    private GameObject foodVisual;
    [SerializeField] private Sprite[] moodSprites;
    [SerializeField] private SpriteRenderer moodSr;
    private void Start()
    {
        GetComponent<CustomerMovement>().OnRecievedFood += GotFood;
        GetComponent<CustomerMovement>().OnDoneEating += DoneEating;
        CustomerData.OnMoodChanged += UpdateMoodSprite;
    }
    private void GotFood(FoodRecipeSO recipe)
    {
        foodVisual = new GameObject("food visual");
        foodVisual.transform.parent = foodParent;
        foodVisual.transform.localPosition = Vector3.zero;
        foodVisual.AddComponent<SpriteRenderer>();
        foodVisual.GetComponent<SpriteRenderer>().sprite = recipe.sprite;
        moodSr.gameObject.SetActive(false);
    }
    private void DoneEating()
    {
        Destroy(foodVisual);
    }

    private void UpdateMoodSprite()
    {
        CustomerData.Moods mood = GetComponent<CustomerData>().GetMood();
        switch (mood)
        {
            case CustomerData.Moods.Content:
                moodSr.sprite = moodSprites[0];
                break;
            case CustomerData.Moods.Impatient:
                moodSr.sprite = moodSprites[1];
                break;
            case CustomerData.Moods.Frustrated:
                moodSr.sprite = moodSprites[2];
                break;
            case CustomerData.Moods.Angry:
                moodSr.sprite = moodSprites[3];
                break;
        }
    }
    public void OnDestroy()
    {
        CustomerData.OnMoodChanged -= UpdateMoodSprite;
    }
}
