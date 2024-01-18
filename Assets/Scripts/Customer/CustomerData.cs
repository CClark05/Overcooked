using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerData : MonoBehaviour
{
    public static Action<FoodRecipeSO> OnLostPatience;
    public Action<FoodRecipeSO> OnFoodReady;
    public static Action OnMoodChanged;
    public enum Moods
    {
        Content, 
        Impatient,
        Frustrated, 
        Angry
    }
    [SerializeField] private Moods mood;
    private static List<Moods> totalMoods = new List<Moods>();
    public static Moods averageMood { get; private set; }
    [SerializeField] private FoodRecipeSO recipe;
    [SerializeField] private float patienceLevel;
    private bool hasFood;
    private float _patienceLevel;
    private float minPatience = 5; //20
    private float maxPatience = 10; //40
    private float eatTime = 3;
    private float totalPatience;
    public bool hasUI;
    private void Awake()
    {
        mood = Moods.Content;
        patienceLevel = UnityEngine.Random.Range(minPatience, maxPatience);
        _patienceLevel = patienceLevel;
        totalPatience = patienceLevel * 4;
    }
    private void Start()
    {
        AddMood();
    }
    private void Update()
    {

        if (hasFood || GameManager.Instance.GetState() == GameManager.States.GameOver) return;
        patienceLevel -= Time.deltaTime;
        totalPatience -= Time.deltaTime;
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
                totalMoods.Remove(Moods.Content);
                AddMood();
                break;
            case Moods.Impatient:
                mood = Moods.Frustrated;
                totalMoods.Remove(Moods.Impatient);
                AddMood();
                break;
            case Moods.Frustrated:
                mood = Moods.Angry;
                totalMoods.Remove(Moods.Frustrated);
                AddMood();
                break;
            case Moods.Angry:
                OnLostPatience?.Invoke(recipe);
                OnFoodReady?.Invoke(null);
                break;
        }
    }
    public float GetPatiencePercentage()
    {
        return 1-(totalPatience / (_patienceLevel * 4));
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
    private void AddMood()
    {
        totalMoods.Add(mood);
        averageMood = FindDominantMood();
        OnMoodChanged?.Invoke();
    }
    public static Moods FindDominantMood()
    {
        if (totalMoods.Count == 0) return Moods.Content;
        var dominantMood = totalMoods.GroupBy(x=>x).OrderByDescending(g=> g.Count()).Select(g => g.Key)
            .FirstOrDefault();
        return dominantMood;
    }
    public static string GetMoodColor(Moods mood)
    {
        switch (mood)
        {
            case Moods.Content:
                return "#3e8948";
            case Moods.Impatient:
                return "#fee761";
            case Moods.Frustrated:
                return "#d77643";
            case Moods.Angry:
                return "#a22633";
        }
        return null;
    }
}
   



