using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable 
{
    public Vector2 MovementDirection { get; }
    public void LockMovement();
    public void UnLockMovement();
}
