using System;
using TMPro;
using UnityEngine;


public class DeliveryCounterEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private TextMeshProUGUI scorePopupText;
    private Vector3 originalScorePopPosition;

    private void Awake()
    {
        originalScorePopPosition = scorePopupText.rectTransform.localPosition;
    }

    private void Start()
    {
        DeliveryCounterInteract.OnFoodDelivered += () =>
        {
            particles.Play();
            scorePopupText.text = "+" + (ScoreCalculator.Instance.score - ScoreCalculator.Instance.lastScore);
            ScorePopupAnimation();
        };
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            particles.Play();
            ScorePopupAnimation();
        }
    }

    private void ScorePopupAnimation()
    {
        float moveTime = 0.4f;
        float fadeTime = 0.3f;
        LeanTween.value(scorePopupText.gameObject, f => scorePopupText.color = new Color(222 / 255f, 158 / 255f, 65 / 255f, f), 0, 1, fadeTime);
        LeanTween.moveLocalY(scorePopupText.gameObject, 0.8f, moveTime).setEaseOutSine().setDelay(0.1f).setOnComplete(() =>
        {
            LeanTween.value(scorePopupText.gameObject, f => scorePopupText.color = new Color(222 / 255f, 158 / 255f, 65 / 255f, f), 1, 0, fadeTime).setOnComplete(() =>
            {
                scorePopupText.rectTransform.localPosition = originalScorePopPosition;
            });
        });
    }
}
