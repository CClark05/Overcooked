using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private SliderUI soundEffectsSlider;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private SliderUI musicSlider;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button backButton;
    private void Start()
    {
        soundEffectsSlider.OnSliderChanged += (float v) =>
        {
            soundEffectsText.text = (Mathf.Ceil(v * 100)).ToString() + "%";
        };
        musicSlider.OnSliderChanged += (float v) =>
        {
            musicText.text = (Mathf.Ceil(v * 100)).ToString() + "%";
        };
        backButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scenes.MainMenu);
        });


    }

}
