using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance { get; private set; }

    private KeyCode interactKey = KeyCode.E;
    private KeyCode cutKey = KeyCode.F;
    private KeyCode pauseKey = KeyCode.Escape;
    private KeyCode dashKey = KeyCode.Space;
    public event EventHandler onInteractPressed;
    public event EventHandler onCutPressed;
    public event EventHandler onPausePressed;
    public event EventHandler onDashPressed;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            onInteractPressed?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(cutKey))
        {
            onCutPressed?.Invoke(this, EventArgs.Empty);
        }
        if(Input.GetKeyDown(pauseKey))
        {
            onPausePressed?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(dashKey))
        {
            onDashPressed?.Invoke(this, EventArgs.Empty);
        }
    }

}
