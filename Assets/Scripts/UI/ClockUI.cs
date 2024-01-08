using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Image clockImage;
    
    private void Update()
    {
        if (GameManager.Instance.GetState() == GameManager.States.Playing || GameManager.Instance.GetState() == GameManager.States.GameOver)
        {
            clockImage.fillAmount = GameManager.Instance.GetGameTimerNormalized();
        }
    }
}
