using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Sprite[] clockSprites;
    [SerializeField] private Image clockImage;
    private void Start()
    {
        timeText.text = FormatTime(Mathf.RoundToInt(GameManager.Instance.GetGameTimeMax()));
        CustomerData.OnMoodChanged += UpdateMood;
    }

    private void Update()
    {
        
        if (GameManager.Instance.GetState() == GameManager.States.Playing)
        {
            timeText.text = FormatTime(Mathf.RoundToInt(GameManager.Instance.gameTimer));
        }
    
    }
    private string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;

        string formattedTime = string.Format("{0}:{1:D2}", minutes, remainingSeconds);
        return formattedTime;
    }
    private void UpdateMood()
    {
        switch (CustomerData.FindDominantMood())
        {
            case CustomerData.Moods.Content:
                clockImage.sprite = clockSprites[0];
                break;
            case CustomerData.Moods.Impatient:
                clockImage.sprite = clockSprites[1];
                break;
            case CustomerData.Moods.Frustrated:
                clockImage.sprite = clockSprites[2];
                break;
            case CustomerData.Moods.Angry:
                clockImage.sprite = clockSprites[3];
                break;
        }

    }
}
