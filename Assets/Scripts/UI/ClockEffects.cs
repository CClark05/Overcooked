using System;
using TMPro;
using UnityEngine;


public class ClockEffects : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private void Start()
    {
        GetComponent<ClockUI>().OnTimeLow += () =>
        {
            LeanTween.value(timeText.gameObject, new Color(1, 1, 1), new Color(126f / 255f, 38f / 255f, 51f / 255f), 0.6f).setOnUpdate((Color color) =>
            {
                timeText.color = color;
            }).setLoopPingPong();
        };
        GameManager.Instance.OnGameEnded += () => LeanTween.cancel(timeText.gameObject);
    }
}