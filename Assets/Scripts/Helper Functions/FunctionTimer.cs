using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FunctionTimer
{
    public static FunctionTimer Create(Action func, float timer) 
    {
        GameObject newObject = new GameObject("FunctionTimer", typeof(BehaviourHook));
        FunctionTimer functionTimer = new FunctionTimer(func, timer, newObject);
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
    private GameObject obj;
    private FunctionTimer(Action func, float timer, GameObject obj) 
    {
        this.func = func;
        this.timer = timer;
        this.obj = obj;
    }

    public void Update()
    {
        if (destroyTimer) return;
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            func?.Invoke();
            destroyTimer = true;
            UnityEngine.Object.Destroy(obj);
        }
    }
}
