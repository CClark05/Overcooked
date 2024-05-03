using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Teleporter other;
    [SerializeField] private float delayTime = 2f;
    public float DelayTime => delayTime;
    private const string timerName = "timer";
    public event Action OnEnter;
    public event Action OnExit;
    public event Action OnTeleported;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITeleportable teleport))
        {
            OnEnter?.Invoke();
            FunctionTimer.Create(() =>
            {
                teleport.LockPosition();
                teleport.Teleport(other.transform.position, () => teleport.UnLockPosition());
                OnTeleported?.Invoke();
            }, delayTime, timerName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnExit?.Invoke();
        FunctionTimer.StopTimer(timerName);
    }
}