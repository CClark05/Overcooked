using UnityEditor.SceneManagement;
using UnityEngine;


public class CustomerAnimation : SpriteAnimator
{
    private CustomerMovement movement;

    private new void Awake()
    {
        base.Awake();
        movement = GetComponent<CustomerMovement>();
    }

    private enum AnimationStates
    {
        IdleDown,
        IdleUp,
        IdleSide,
        RunDown,
        RunUp,
        RunSide,
        
    }

    private AnimationStates DetermineAnimationState()
    {
        Vector2 lastDirection = movement.LastUpdatedDirection;
        Vector2 direction = movement.MovementDirection;
        if (direction == Vector2.zero)
        {
            if (Mathf.Abs(lastDirection.x) > 0) return AnimationStates.IdleSide;
            return lastDirection.y > 0 ? AnimationStates.IdleUp : AnimationStates.IdleDown;
        }

        if (direction.y > 0) return AnimationStates.RunUp;
        if (direction.y < 0) return AnimationStates.RunDown;
        return AnimationStates.RunSide;
        
    }

    private new void Update()
    {
        base.Update();
        SetAnimation(animations[(int)DetermineAnimationState()]);
    }
}