using UnityEngine;

public class StateMachine
{
    private State _currentState;

    public StateMachine(State currentState)
    {
        _currentState = currentState;
        _currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        _currentState.Exit();

        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState.Update();
    }
}
