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
        Debug.Log("test");
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
        Game,
        Options
    }
 
    public void LoadScene(Scenes scene)
    {
        /**
        if (transition == null)
        {
            SceneManager.LoadScene(scene.ToString());
            Debug.Log("No Scene Transition found");
            return;
        }
        transition.GetComponent<ITransition>().OnExit( ()=> SceneManager.LoadSceneAsync(scene.ToString()));
        */
        StartCoroutine(LoadSceneAsync(scene));
    }

    private IEnumerator LoadSceneAsync(Scenes sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName.ToString());
        scene.allowSceneActivation = false;
        while (scene.progress < 0.9f) yield return null;
        transition.GetComponent<ITransition>().OnExit(() => scene.allowSceneActivation = true);
    }
}
