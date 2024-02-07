using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteAnimator : MonoBehaviour
{
    [SerializeField] protected List<SpriteAnimation> spriteAnimations;
    private SpriteAnimation currentAnimation;
    protected SpriteRenderer sr;
    private int currentSpriteIndex = 0;
    private float timer = 0;
    private float frameRate;
    public void Init(float frameRate, SpriteAnimation startingAnimation)
    {
        sr = GetComponent<SpriteRenderer>();
        this.frameRate = frameRate;
        SetAnimation(startingAnimation);
        
    }
    protected void Update()
    {
        if(currentAnimation == null)
        {
            Debug.LogError("current animation is null");
            return;
        }
        timer += Time.deltaTime;
        if(timer >= 1f / frameRate)
        {
            timer = 0;
            currentSpriteIndex = (currentSpriteIndex + 1) % currentAnimation.sprites.Length;
            sr.sprite = currentAnimation.sprites[currentSpriteIndex];
        }
    }
    
    protected void SetAnimation(SpriteAnimation animation)
    {
        if (currentAnimation != animation) {
            this.currentAnimation = animation;
            sr.sprite = animation.sprites[0];
        }
    }
}
