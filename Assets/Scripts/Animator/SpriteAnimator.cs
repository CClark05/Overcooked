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
                    currentAnimation = nextAnimation;
                    currentSpriteIndex = 0;
                }
            }

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
    protected void SetAnimation(SpriteAnimation animation, SpriteAnimation nextAnimation)
    {
        this.nextAnimation = nextAnimation;
        if (currentAnimation != animation)
        {
            this.currentAnimation = animation;
            sr.sprite = animation.sprites[0];
        }
    }
}
