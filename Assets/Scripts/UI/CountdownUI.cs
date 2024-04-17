using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    public string CountdownText
    {
        get => countdownText.text;
        set
        {
            if (countdownText.text.Equals(value)) return;
            countdownText.text = value;
            OnNumberChanged?.Invoke();
        }
    }
    
    private GameManager gameManager;
    public event Action OnNumberChanged;
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnStateChanged += Instance_OnStateChanged;
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if(gameManager.GetState() == GameManager.States.Countdown)
        {
            countdownText.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if(gameManager.GetState() == GameManager.States.Countdown)
        {
            CountdownText = (Mathf.Ceil(gameManager.GetCountdownTimer()).ToString());
        }
    }

}
