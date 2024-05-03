using System;
using UnityEngine;

public interface ITeleportable
{
    public void Teleport(Vector2 position, Action OnTeleported);
    public void LockPosition();
    public void UnLockPosition();
}