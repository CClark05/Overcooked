using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Scale : Button_Base
{
    [SerializeField] private float scaleFactor;
    [SerializeField] private float animationDuration;


    public override void OnMouseEnter()
    {
        LeanTween.scale(gameObject, new Vector3(scaleFactor, scaleFactor, scaleFactor), animationDuration);
    }
    public override void OnMouseLeave()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), animationDuration);
    }
}
    

