using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodVisualizerUI : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Start()
    {
        CustomerData.OnMoodChanged += UpdateImage;
        image.color = HexToColor(CustomerData.GetMoodColor(CustomerData.Moods.Content));
    }

    private void UpdateImage()
    { 
        image.color = HexToColor(CustomerData.GetMoodColor(CustomerData.FindDominantMood()));
    }
    
    private Color HexToColor(string hexCode)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hexCode, out color);
        return color;
    }
}
