using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] 
public abstract class SpriteAnimator : MonoBehaviour
{
    [SerializeField] protected SpriteAnimation[] animations;
    private SpriteAnimation currentAnimation;
    private SpriteRenderer sr;
    private float timer;
    private int currentFrame;
    private Sprite startingSprite;
    protected void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        startingSprite = sr.sprite;
    }
    public void SetAnimation(SpriteAnimation animation)
    {
        if (animation == currentAnimation) return;
        if (currentAnimation != null && currentAnimation.isLocked && !currentAnimation.GetIsDone()) return;
        currentAnimation = animation;
        currentAnimation.SetIsDone(false);
        currentFrame = 0;
        timer = 0;
        sr.sprite = animation.sprites[0];
        if (currentAnimation.lockMovement)
        {
            if(TryGetComponent(out IMoveable moveable))
            {
                moveable.LockMovement();
            }
            else
            {
                Debug.LogError("No IMoveable component found");
            }
        }
    }
    public void StopAnimation()
    {
        if (currentAnimation != null)
        {
            currentAnimation = null;
            sr.sprite = startingSprite;
        }

    }
    protected void Update()
    {
        if (currentAnimation == null) return;
        timer += Time.deltaTime;

        if(timer >= 1f / currentAnimation.frameRate)
        {

            currentFrame++;
            if(currentFrame >= currentAnimation.sprites.Length)
            {
                if (currentAnimation.isLooping)
                {
                    currentFrame = 0;
                }
                else
                {
                    currentAnimation.OnComplete?.Invoke();
                    currentAnimation.SetIsDone(true);
                    if(currentAnimation.lockMovement) GetComponent<IMoveable>().UnLockMovement();
                    currentAnimation = null;
                    sr.sprite = startingSprite;
                    return;
                }
            }
            sr.sprite = currentAnimation.sprites[currentFrame];
            timer = 0;
            
        }
    }
}
