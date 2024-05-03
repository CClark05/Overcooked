using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Rendering;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    [SerializeField] private GameObject transition;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        transition.GetComponent<ITransition>().OnEnter();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene _, LoadSceneMode mode)
    {
        transition.GetComponent<ITransition>().OnEnter();
    }

    public enum Scenes
    {
        MainMenu,
        Options,
        LevelSelect,
        Level1,
    }
 
    public void LoadScene(Scenes scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    public void ReloadCurrentScene()
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
    }

    private IEnumerator LoadSceneAsync(Scenes sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName.ToString());
        scene.allowSceneActivation = false;
        while (scene.progress < 0.9f) yield return null;
        transition.GetComponent<ITransition>().OnExit(() => scene.allowSceneActivation = true);
    }
    
    private IEnumerator LoadSceneAsync(String sceneName)
    {
        Debug.Log("test");
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        while (scene.progress < 0.9f) yield return null;
        transition.GetComponent<ITransition>().OnExit(() => scene.allowSceneActivation = true);
    }
}
