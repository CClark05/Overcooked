using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;
    public Action OnGameStarted;
    public enum States
    {
        WaitingToStart,
        Countdown,
        Playing,
        GameOver
    }
    private States state;

    private float waitingToStartTimer = 1;
    private float countdownTimer = 3;
    private float gameTimerMax = 180;
    private float gameTimer;

    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = States.WaitingToStart;
    }
    private void Start()
    {
        UserInput.Instance.onPausePressed += UserInput_onPausePressed;
    }

    private void UserInput_onPausePressed(object sender, EventArgs e)
    {
        TogglePause();
    }
    public void TogglePause()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            isGamePaused = true;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            return;
        }
        Time.timeScale = 1;
        isGamePaused = false;
        OnGameResumed?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        switch (state)
        {
            case States.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0)
                {
                    state = States.Countdown;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case States.Countdown:
                countdownTimer -= Time.deltaTime;
                if (countdownTimer < 0)
                {
                    state = States.Playing;
                    gameTimer = gameTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    OnGameStarted?.Invoke();
                }
                break;
            case States.Playing:
                gameTimer -= Time.deltaTime;
                if (gameTimer < 0)
                {
                    state = States.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case States.GameOver:
                break;
        }
    }
    public float GetCountdownTimer()
    {
        return countdownTimer;
    }
    public States GetState()
    {
        return state;
    }
    public float GetGameTimerNormalized()
    {
        return 1-(gameTimer / gameTimerMax);
    }
}
