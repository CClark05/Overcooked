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
            //sr.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.75f);
            sr.material = MaterialsManager.Instance.selectedMaterial;
            counterSr.material = MaterialsManager.Instance.selectedMaterial;
        }
        else
        {
            //sr.color = spriteColor;
            sr.material = MaterialsManager.Instance.defaultMaterial;
            counterSr.material = MaterialsManager.Instance.defaultMaterial;
        }

    }
}
