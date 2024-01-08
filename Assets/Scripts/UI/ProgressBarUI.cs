using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    private IHasProgressBar Counter;
    [SerializeField] private Image progressBar;
    private void Awake()
    {
        Counter = GetComponent<IHasProgressBar>();
    }
    private void Start()
    {
        Counter.OnProgressChanged += IHasProgress_OnProgressChanged;
        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgressBar.OnProgressChangedEventArgs e)
    {
        progressBar.fillAmount = e.percentProgress;
        if(e.percentProgress == 0 || e.percentProgress == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
    private void Show()
    {
        progressBar.rectTransform.parent.gameObject.SetActive(true);
    }
    private  void Hide()
    {
        progressBar.rectTransform.parent.gameObject.SetActive(false);
    }
}
