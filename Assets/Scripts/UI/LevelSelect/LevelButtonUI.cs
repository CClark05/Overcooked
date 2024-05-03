using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scenes scene;
    [SerializeField] private bool isLocked;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] Sprite unLockedSprite;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private LevelStar[] stars;
    private Image image;
    private Button_Scale button;
    
    public bool IsLocked
    {
        get => isLocked;
        set
        {
            if (Equals(value, isLocked)) return;
            isLocked = value;
            UpdateLockStatus();
        }
    }
    
    private void Awake()
    {
        button = GetComponent<Button_Scale>();
        image = GetComponent<Image>();
    }

    private void Start()
    {
        button.OnClick.AddListener(() =>
        {
            SceneLoader.Instance.LoadScene(scene);
        });
        UpdateLockStatus();
    }

    private void UpdateLockStatus()
    {
        image.sprite = isLocked ? lockedSprite : unLockedSprite;
        levelText.enabled = !isLocked;
        button.enabled = !isLocked;
        if (!isLocked)
        {
            foreach (LevelStar star in stars)
            {
                star.gameObject.SetActive(true);
            }
        }
    }
}
