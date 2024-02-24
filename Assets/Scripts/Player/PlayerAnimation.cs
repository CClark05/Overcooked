using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : SpriteAnimator
{
    private PlayerMovement playerMovement;
    private PlayerLifeCycle playerLifeCycle;
    public Action OnDeathAnimationDone;
    [SerializeField] private AnimationCurve deathCurve;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerLifeCycle = GetComponent<PlayerLifeCycle>();
        base.Init(spriteAnimations[0]);
    }
    private void Start()
    {
        playerMovement.OnDash += DashAnimation;
        playerLifeCycle.OnDeath += DeathAnimation;
    }
    new private void Update()
    {
        base.Update();
        if (playerMovement.isDashing) return;
        if((Vector2)playerMovement.GetCurrentDirection() == Vector2.zero)
        {
            IdleAnimation();
        }
        else
        {
            RunAnimation();
        }

    }
    private void DashAnimation(Vector2 direction)
    {
        if (direction.x > 0)
        {
            SetAnimation(spriteAnimations[4]);
        }
        else if (direction.x < 0)
        {
            SetAnimation(spriteAnimations[5]);
        }
        else if (direction.y > 0)
        {
            SetAnimation(spriteAnimations[6]);
        }
        else
        {
            SetAnimation(spriteAnimations[7]);
        }
    }
    private void RunAnimation()
    {
        Vector2 direction = playerMovement.GetCurrentDirection();
        if (direction.x > 0)
        {
            SetAnimation(spriteAnimations[14]);
        }
        else if (direction.x < 0)
        {
            SetAnimation(spriteAnimations[15]);
        }
        else if (direction.y > 0)
        {
            SetAnimation(spriteAnimations[13]);
        }
        else if(direction.y < 0)
        {
            SetAnimation(spriteAnimations[12]);
        }
    }
    private void DeathAnimation(Vector2 direction)
    {
        CancelAnimation();
        if (direction.x > 0)
        {
            SetAnimation(spriteAnimations[11]);
        }
        else if (direction.x < 0)
        {
            SetAnimation(spriteAnimations[10]);
        }
        else if (direction.y > 0)
        {
            SetAnimation(spriteAnimations[9]);
        }
        else
        {
            SetAnimation(spriteAnimations[8]);
        }
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.6f).setEase(deathCurve).setOnComplete(() => {
            OnDeathAnimationDone?.Invoke();
            OverrideSetAnimation(spriteAnimations[0]);     
        });
    }
    
    private void IdleAnimation()
    {

            Vector2 lastDirection = playerMovement.GetLastUpdatedDirection();
            if (lastDirection.x > 0)
            {
                SetAnimation(spriteAnimations[2]);
            }
            else if (lastDirection.x < 0)
            {
                SetAnimation(spriteAnimations[3]);
            }
            else if (lastDirection.y > 0)
            {
                SetAnimation(spriteAnimations[1]);
            }
            else
            {
                SetAnimation(spriteAnimations[0]);
            }

    }

}
