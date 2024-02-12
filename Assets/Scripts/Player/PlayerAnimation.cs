using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : SpriteAnimator
{
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        base.Init(spriteAnimations[0]);
    }
    private void Start()
    {

        playerMovement.OnDash += DashAnimation;

    }
    private void DashAnimation(Vector2 direction)
    {
        if (direction.x > 0)
        {
            SetAnimation(spriteAnimations[4], spriteAnimations[2]);
        }
        else if (direction.x < 0)
        {
            SetAnimation(spriteAnimations[5], spriteAnimations[3]);
        }
        else if (direction.y > 0)
        {
            SetAnimation(spriteAnimations[6], spriteAnimations[1]);
        }
        else
        {
            SetAnimation(spriteAnimations[7], spriteAnimations[0]);
        }
    }
    new private void Update()
    {
        base.Update();
        if((Vector2)playerMovement.GetCurrentDirection() == Vector2.zero)
        {
            Vector2 lastDirection = playerMovement.GetLastUpdatedDirection();
            if(lastDirection.x > 0)
            {
                SetAnimation(spriteAnimations[2]);
            }else if(lastDirection.x < 0)
            {
                SetAnimation(spriteAnimations[3]);
            }else if(lastDirection.y > 0)
            {
                SetAnimation(spriteAnimations[1]);
            }
            else
            {
                SetAnimation(spriteAnimations[0]);
            }
        }
    }
}
