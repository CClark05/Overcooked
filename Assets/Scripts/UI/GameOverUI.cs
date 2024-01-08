using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnStateChanged += Instance_OnStateChanged;
        gameObject.SetActive(false);
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (gameManager.GetState() == GameManager.States.GameOver)
        {
            gameObject.SetActive(true);
            scoreText.text = DeliveryManager.Instance.GetAmountOfRecipesDelivered().ToString();
            return;
        }
        gameObject.SetActive(false);
    }

}
