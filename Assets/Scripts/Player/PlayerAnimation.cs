using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum Animations
    {
        PlayerIdle,
        PlayerDeath,
    }
    private Animations currentAnimation;
    private Animator animator;
    private List<(Action, Animations)> animationEvents = new List<(Action, Animations)>();
    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentAnimation = Animations.PlayerIdle;
    }

    public void PlayAnimation(Animations animation, Action OnAnimationDone)
    {
        if (animation == currentAnimation) return;
        animator.Play(animation.ToString());
        currentAnimation = animation;
        var tuple = (OnAnimationDone, animation);
        if (animationEvents.Contains(tuple)) return;
        animationEvents.Add((OnAnimationDone, animation));
    }

    public void PlayAnimation(Animations animation)
    {
        if (animation == currentAnimation) return;
        animator.Play(animation.ToString());
        currentAnimation = animation;
    }

    public void AnimationDone(Animations animation)
    {
        foreach((Action action, Animations actionAnimation) in animationEvents)
        {
            if(actionAnimation == animation)
            {
                action?.Invoke();
            }
        }
    }
}
