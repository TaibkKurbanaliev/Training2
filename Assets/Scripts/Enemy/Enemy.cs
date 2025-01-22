using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private State _startState;
    [SerializeField] private List<State> _states = new();
    [SerializeField] private StateMachine _stateMachine;

    private void Start()
    {
        _stateMachine = new StateMachine(_startState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}
