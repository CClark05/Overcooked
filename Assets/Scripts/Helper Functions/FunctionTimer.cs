using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FunctionTimer
{
    public static FunctionTimer Create(Action func, float timer) 
    {
        FunctionTimer functionTimer = new FunctionTimer(func, timer);
        GameObject newObject = new GameObject("FunctionTimer", typeof(BehaviourHook));
        newObject.GetComponent<BehaviourHook>().onUpdate = functionTimer.Update;
        return functionTimer;
    }

    private class BehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            onUpdate?.Invoke();
        }
    }
    private Action func;
    private float timer;
    private bool destroyTimer;
    private FunctionTimer(Action func, float timer) 
    {
        this.func = func;
        this.timer = timer;
    }

    public void Update()
    {
        if (destroyTimer) return;
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            func?.Invoke();
            destroyTimer = true;
        }
    }
}
