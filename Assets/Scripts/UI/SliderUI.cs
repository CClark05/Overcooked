using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public Action<float> OnSliderChanged;
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener((v) =>
        {
            OnSliderChanged?.Invoke(v);
        });
    }
}
