using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour, IGameObjectPooling
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float alphaDecay;
    [SerializeField] private float startingAlpha;
    private SpriteRenderer sr;
    private Color startingColor;
    
    private float alpha;
    private float timer;
    public static Transform parent;
    public GameObjectPool Pool {get; set;}
    
    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        startingColor = sr.color;
        alpha = startingAlpha;
        sr.color = new Color(startingColor.r, startingColor.g, startingColor.b, alpha);
        sr.sprite = parent.GetComponent<SpriteRenderer>().sprite;
        transform.position = parent.position;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        alpha *= alphaDecay;
        sr.color = new Color(startingColor.r, startingColor.g, startingColor.b, alpha);
        if(timer >= lifeTime)
        {
            Pool.ReturnToPool(this.gameObject);
        }
    }
}
