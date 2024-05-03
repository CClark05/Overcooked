using UnityEngine;
using UnityEngine.UI;

public class LevelStar : MonoBehaviour
{
    [SerializeField] private bool isGold;

    public bool IsGold
    {
        get => isGold;
        set
        {
            if (Equals(isGold, value)) return;
            isGold = value;
            UpdateSprite();
        }
    }

    [SerializeField] private Sprite goldStarSprite;
    [SerializeField] private Sprite greyStarSprite;
    private Image image;

    private void Awake() => image = GetComponent<Image>();

    private void OnEnable() => UpdateSprite();

    private void UpdateSprite() => image.sprite = isGold ? goldStarSprite : greyStarSprite;
}