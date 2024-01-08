using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button resumeButton;
    private void Awake()
    {
        menuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneLoader.LoadScene(SceneLoader.Scenes.MainMenu);
        });
        resumeButton.onClick.AddListener(() => 
        {
            GameManager.Instance.TogglePause();
        });
    }
private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
        gameObject.SetActive(false);
    }

    private void GameManager_OnGameResumed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }
}



