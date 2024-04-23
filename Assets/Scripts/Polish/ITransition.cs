using System;

public interface ITransition
{
    public void OnExit(Action OnComplete);
    public void OnEnter();
}