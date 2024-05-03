using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class TeleporterVisual : MonoBehaviour
{
    private Teleporter teleporter;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite altSprite;
    private Sprite originalSprite;
    [SerializeField] private Light2D light;
    private float originalLightIntensity;
    public Action OnTeleporterEnabledAgain;
    private void Awake()
    {
        teleporter = GetComponent<Teleporter>();
        originalLightIntensity = light.intensity;
        originalSprite = sr.sprite;
    }

    private void Start()
    {
        teleporter.OnEnter += () =>
        {
            sr.sprite = altSprite;
            LeanTween.scale(sr.gameObject, new Vector3(1.1f, 1.1f, 1.1f), teleporter.DelayTime * 0.2f).setOnComplete(() =>
            {
                LeanTween.value(sr.gameObject, f => light.intensity = f, originalLightIntensity, 0, teleporter.DelayTime * 0.5f);
                LeanTween.scale(sr.gameObject, Vector3.zero, teleporter.DelayTime * 0.5f).setEaseInQuint();
            }).setDelay(teleporter.DelayTime * 0.3f).setEaseOutQuint();
        };
        teleporter.OnExit += () =>
        {
            sr.sprite = originalSprite;
            FunctionTimer.Create(() =>
            {
                if (Vector3.Distance(sr.transform.localScale, Vector3.zero) < 0.06f)
                {
                    float zoomBackInTime = 1f;
                    LeanTween.scale(sr.gameObject, Vector3.one, zoomBackInTime).setEaseInOutCubic().setDelay(0.5f).setOnComplete(() => OnTeleporterEnabledAgain?.Invoke());
                    LeanTween.value(sr.gameObject, f => light.intensity = f, 0, originalLightIntensity, zoomBackInTime);
                    return;
                }

                LeanTween.cancel(sr.gameObject);
                LeanTween.scale(sr.gameObject, Vector3.one, 0.2f / sr.transform.localScale.x);
                LeanTween.value(sr.gameObject, f => light.intensity = f, light.intensity, originalLightIntensity, 0.2f / light.intensity);
            }, 0.1f);
        };
    }
}