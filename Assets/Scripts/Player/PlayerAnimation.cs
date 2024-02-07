using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : SpriteAnimator
{
    private PlayerMovement playerMovement;
    private void Awake()
    {
        base.Init(5, spriteAnimations[0]);
        playerMovement = GetComponent<PlayerMovement>();
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
