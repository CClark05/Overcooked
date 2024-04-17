using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    private IHasProgressBar Counter;
    [SerializeField] private Image progressBar;
    public Action OnHideBar;
    public Action OnShowBar;
    private void Awake()
    {
        Counter = GetComponent<IHasProgressBar>();
    }
    private void Start()
    {
        Counter.OnProgressChanged += IHasProgress_OnProgressChanged;
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgressBar.OnProgressChangedEventArgs e)
    {
        progressBar.fillAmount = e.percentProgress;
        if(e.percentProgress == 0 || e.percentProgress == 1)
        {
            OnHideBar?.Invoke();
        }
        else
        {
            OnShowBar?.Invoke();
        }
    }

    public void ChangeColor(Color color)
    {
        progressBar.color = color;
    }
}
