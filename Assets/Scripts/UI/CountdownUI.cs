using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private GameManager gameManager;
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
            return;
        }
        countdownText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(gameManager.GetState() == GameManager.States.Countdown)
        {
            countdownText.text = (Mathf.Ceil(gameManager.GetCountdownTimer()).ToString());
        }
    }

}
