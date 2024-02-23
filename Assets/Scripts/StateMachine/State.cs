using UnityEngine;

public abstract class State
{
    public bool Completed;
    public abstract void OnEnter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void OnExit();

    public void CompleteState()
    {
        Completed = true;
    }
    public void Enter()
    {
        OnEnter();
    }
    public void Exit()
    {
        Completed = false;
        OnExit();
    }
}
