using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button_Base menuButton;
    [SerializeField] private Button_Base resumeButton;
    [SerializeField] private Button_Base quitButton;
    private void Awake()
    {
        menuButton.OnClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneLoader.Instance.LoadScene(SceneLoader.Scenes.MainMenu);
        });
        resumeButton.OnClick.AddListener(() => 
        {
            GameManager.Instance.TogglePause();
        });
        quitButton.OnClick.AddListener(() =>
        {
            Debug.Log("Quit");
            Application.Quit();
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



