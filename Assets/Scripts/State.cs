using UnityEngine;

public abstract class State
{
    protected readonly StateMachine StateMachine;

    public State(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void Enter(Player player) { }
    public virtual void Exit(Player player) { }
    public virtual void Update(Player player) { }
}
