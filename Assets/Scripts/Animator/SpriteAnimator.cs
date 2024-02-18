using System;
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
    private SpriteAnimation nextAnimation;
    private Action OnComplete;
    private bool lockAnimation;
    public void Init(SpriteAnimation startingAnimation)
    {
        sr = GetComponent<SpriteRenderer>();
        SetAnimation(startingAnimation);
    }
    protected void Update()
    {
        if (currentAnimation == null)
        {
            Debug.LogError("current animation is null");
            return;
        }
        timer += Time.deltaTime;
        if (timer >= 1f / currentAnimation.frameRate)
        {
            timer = 0;
            currentSpriteIndex++;

            if (currentSpriteIndex >= currentAnimation.sprites.Length)
            {
                if (currentAnimation.looping)
                {
                    currentSpriteIndex = 0;
                }
                else
                {
                    if (OnComplete != null) OnComplete.Invoke();
                    currentAnimation = nextAnimation;
                    currentSpriteIndex = 0;
                    
                }
            }

            sr.sprite = currentAnimation.sprites[currentSpriteIndex];
        }
    }
    protected void SetAnimation(SpriteAnimation animation)
    {
        if (currentAnimation != null && currentAnimation.locked || lockAnimation) return;
        if (currentAnimation != animation) {
            this.currentAnimation = animation;
            sr.sprite = animation.sprites[0];
        }
    }
    protected void OverrideSetAnimation(SpriteAnimation animation)
    {
        if (currentAnimation != animation)
        {
            this.currentAnimation = animation;
            sr.sprite = animation.sprites[0];
        }
    }
    protected void SetAnimation(SpriteAnimation animation, SpriteAnimation nextAnimation)
    {
        if (currentAnimation != null && currentAnimation.locked || lockAnimation) return;
        this.nextAnimation = nextAnimation;
        OnComplete = null;
        if (currentAnimation != animation)
        {
            this.currentAnimation = animation;
            sr.sprite = animation.sprites[0];
        }
    }
    protected void SetAnimation(SpriteAnimation animation, SpriteAnimation nextAnimation, Action OnComplete)
    {
        if (currentAnimation != null && currentAnimation.locked || lockAnimation) return;
        this.nextAnimation = nextAnimation;
        if (currentAnimation != animation)
        {
            this.OnComplete = OnComplete;
            this.currentAnimation = animation;
            sr.sprite = animation.sprites[0];
        }
    }
    protected void CancelAnimation()
    {
        currentAnimation = null;
    }
    protected void ToggleLockAnimation()
    {
        if (lockAnimation == false)
        {
            lockAnimation = true;
            return;
        }
        lockAnimation = false;
    }
    


}
