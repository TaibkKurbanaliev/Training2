using UnityEngine;


public enum PlayerMovementState
{
    Idling,
    Walking,
    Running,
    Sprinting,
    Jumping,
    Falling,
    Strafing,
}

public class PlayerState : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementState CurrentPlayerMovementState {  get; private set; } = PlayerMovementState.Idling;
    

    public void SetPlayerMovementState(PlayerMovementState state)
    {
        CurrentPlayerMovementState = state;
    }
}
