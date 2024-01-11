using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerData : MonoBehaviour
{
    public static Action<FoodRecipeSO> OnLostPatience;
    public Action<FoodRecipeSO> OnFoodReady;
    public enum Moods
    {
        Content, 
        Impatient,
        Frustrated, 
        Angry
    }
    [SerializeField] private Moods mood;
    [SerializeField] private FoodRecipeSO recipe;
    [SerializeField] private float patienceLevel;
    private bool hasFood;
    private float _patienceLevel;
    private float minPatience = 20; //20
    private float maxPatience = 40; //40
    private float eatTime = 3;
    private void Awake()
    {
        mood = Moods.Content;
        patienceLevel = UnityEngine.Random.Range(minPatience, maxPatience);
        _patienceLevel = patienceLevel;
    }
    private void Update()
    {

        if (hasFood) return;
        patienceLevel -= Time.deltaTime;
        if(patienceLevel < 0)
        {
            SetNextMood();
            patienceLevel = _patienceLevel;
        }
    }
    public void SetRecipe(FoodRecipeSO recipe)
    {
        this.recipe = recipe;
    }
    private void SetNextMood()
    {
        switch (mood)
        {
            case Moods.Content:
                mood = Moods.Impatient;
                break;
            case Moods.Impatient:
                mood = Moods.Frustrated;
                break;
            case Moods.Frustrated:
                mood = Moods.Angry;
                break;
            case Moods.Angry:
                OnLostPatience?.Invoke(recipe);
                OnFoodReady?.Invoke(null);
                break;
        }
    }
    public void FoodReady()
    {
        OnFoodReady?.Invoke(recipe);
        hasFood = true;
    }
    public FoodRecipeSO GetRecipe()
    {
        return recipe;
    }
    public Moods GetMood()
    {
        return mood;
    }
    public float GetEatTime()
    {
        return eatTime;
    }


}
