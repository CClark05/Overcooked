using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteAnimatorOLD : MonoBehaviour
{
    [SerializeField] protected List<SpriteAnimationOLD> spriteAnimations;
    private SpriteAnimationOLD _currentAnimationOld;
    protected SpriteRenderer sr;
    private int currentSpriteIndex = 0;
    private float timer = 0;
    private SpriteAnimationOLD _nextAnimationOld;
    private Action OnComplete;
    private bool lockAnimation;
    public void Init(SpriteAnimationOLD startingAnimationOld)
    {
        sr = GetComponent<SpriteRenderer>();
        SetAnimation(startingAnimationOld);
    }
    protected void Update()
    {
        if (_currentAnimationOld == null)
        {
            Debug.LogError("current animation is null");
            return;
        }
        timer += Time.deltaTime;
        if (timer >= 1f / _currentAnimationOld.frameRate)
        {
            timer = 0;
            currentSpriteIndex++;

            if (currentSpriteIndex >= _currentAnimationOld.sprites.Length)
            {
                if (_currentAnimationOld.looping)
                {
                    currentSpriteIndex = 0;
                }
                else
                {
                    if (OnComplete != null) OnComplete.Invoke();
                    _currentAnimationOld = _nextAnimationOld;
                    currentSpriteIndex = 0;
                    
                }
            }

            sr.sprite = _currentAnimationOld.sprites[currentSpriteIndex];
        }
    }
    protected void SetAnimation(SpriteAnimationOLD animationOld)
    {
        if (_currentAnimationOld != null && _currentAnimationOld.locked || lockAnimation) return;
        if (_currentAnimationOld != animationOld) {
            this._currentAnimationOld = animationOld;
            sr.sprite = animationOld.sprites[0];
        }
    }
    protected void OverrideSetAnimation(SpriteAnimationOLD animationOld)
    {
        if (_currentAnimationOld != animationOld)
        {
            this._currentAnimationOld = animationOld;
            sr.sprite = animationOld.sprites[0];
        }
    }
    protected void SetAnimation(SpriteAnimationOLD animationOld, SpriteAnimationOLD nextAnimationOld)
    {
        if (_currentAnimationOld != null && _currentAnimationOld.locked || lockAnimation) return;
        this._nextAnimationOld = nextAnimationOld;
        OnComplete = null;
        if (_currentAnimationOld != animationOld)
        {
            this._currentAnimationOld = animationOld;
            sr.sprite = animationOld.sprites[0];
        }
    }
    protected void SetAnimation(SpriteAnimationOLD animationOld, SpriteAnimationOLD nextAnimationOld, Action OnComplete)
    {
        if (_currentAnimationOld != null && _currentAnimationOld.locked || lockAnimation) return;
        this._nextAnimationOld = nextAnimationOld;
        if (_currentAnimationOld != animationOld)
        {
            this.OnComplete = OnComplete;
            this._currentAnimationOld = animationOld;
            sr.sprite = animationOld.sprites[0];
        }
    }
    protected void CancelAnimation()
    {
        _currentAnimationOld = null;
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
