using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.LoadScene(SceneLoader.Scenes.LevelSelect);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        optionsButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.LoadScene(SceneLoader.Scenes.Options);
        });

    }
}
