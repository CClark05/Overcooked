using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkVisual : CounterVisual
{
    private SinkInteract sinkInteract;
    [SerializeField] private Sprite dirtySinkSprite;
    [SerializeField] private SpriteRenderer sinkSr;
    [SerializeField] private SpriteRenderer counterSr;
    private Sprite originalSprite;
    new private void Awake()
    {
        base.Awake();
        originalSprite = sinkSr.sprite;
        sinkInteract = GetComponent<SinkInteract>();
    }
    new private void Start()
    {
        base.Start();
        sinkInteract.OnHasDirtyPlate += () =>
        {
            sinkSr.sprite = dirtySinkSprite;
        };
        sinkInteract.OnSinkEmpty += () =>
        {
              sinkSr.sprite = originalSprite;
        };
    }

    public override void Instance_onSelectedCounterChanged(object sender, PlayerInteraction.onSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == counterInteract)
        {
            ColorUtility.TryParseHtmlString(selectedColor, out Color newColor);
            sr.color = newColor;
            counterSr.color = newColor;
        }
        else
        {
            sr.color = originalColor;
            counterSr.color = originalColor;
        }

    }
}
