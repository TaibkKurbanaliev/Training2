using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private State _startState;
    [SerializeField] private List<State> _states;
    [SerializeField] private Dictionary<Type,State> _transactionsStates = new();
    [SerializeField] private StateMachine _stateMachine;

}
