using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    private void Start()
    {
        timeText.text = FormatTime(Mathf.RoundToInt(GameManager.Instance.GetGameTimeMax()));
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
}
