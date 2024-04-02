using System;
using System.Collections;
using System.Collections.Generic;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI recipesFailedText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    private TextMeshProUGUI starScore1;
    private TextMeshProUGUI starScore2;
    private TextMeshProUGUI starScore3;
    private GameManager gameManager;
    private LevelData levelData;
    public Image[] Stars { get; private set; }
    private int[] starScores;
    private readonly List<GameObject> goldStars = new List<GameObject>();
    public Action OnShowUI;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        levelData = gameManager.LevelData;
        starScore1 = star1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        starScore2 = star2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        starScore3 = star3.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        levelText.text = "LEVEL " + gameManager.LevelData.level.ToString();
        Stars = new Image[] { star1, star2, star3 };
        starScores = new int[] { levelData.starScore_1, levelData.starScore_2, levelData.starScore_3 };
    }
    private void Start()
    {
        gameManager.OnStateChanged += Instance_OnStateChanged;
        starScore1.text = levelData.starScore_1.ToString();
        starScore2.text = levelData.starScore_2.ToString();
        starScore3.text = levelData.starScore_3.ToString();
        gameObject.SetActive(false);
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (gameManager.GetState() == GameManager.States.GameOver)
        {
            gameObject.SetActive(true);
            recipesDeliveredText.text = "Orders Delivered : " + DeliveryManager.Instance.amountOfRecipesDelivered.ToString();
            recipesFailedText.text = "Orders Failed : " + DeliveryManager.Instance.amountOfRecipesFailed.ToString();
            scoreText.text = ScoreCalculator.Instance.score.ToString();
            highScoreText.text = ScoreCalculator.Instance.highScore.ToString();
            recipesDeliveredText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ScoreCalculator.Instance.scoreFromRecipesDelivered.ToString();
            recipesFailedText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ScoreCalculator.Instance.scoreFromRecipesFailed.ToString();
            for(int i = 0; i<Stars.Length; i++)
            {
                if(ScoreCalculator.Instance.score >= starScores[i])
                {
                    goldStars.Add(Stars[i].gameObject);
                }
            }
            OnShowUI?.Invoke();
            return;
        }
        gameObject.SetActive(false);
    }

    public List<GameObject> GetGoldStars()
    {
        return goldStars;
    }

}
