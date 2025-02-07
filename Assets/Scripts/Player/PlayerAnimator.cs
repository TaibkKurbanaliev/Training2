using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerLocomotionInput))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private float _locomotionBlendSpeed = 0.05f;

    private Animator _animator;
    private PlayerLocomotionInput _playerInput;
    private PlayerState _playerState;

    private int _animIDHorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
    private int _animIDVerticalSpeed = Animator.StringToHash("VerticalSpeed");
    private int _animIDInputMagnitude = Animator.StringToHash("InputMagnitude");

    private Vector2 _currentBlendInput = Vector3.zero;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerLocomotionInput>();
        _playerState = GetComponent<PlayerState>();
    }                                                                     

    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;

        Vector2 inputTarget = isSprinting ? _playerInput.MovementInput * 1.5f : _playerInput.MovementInput;

        _currentBlendInput = Vector2.Lerp(_currentBlendInput, inputTarget, _locomotionBlendSpeed);
        _animator.SetFloat(_animIDHorizontalSpeed, _currentBlendInput.x);
        _animator.SetFloat(_animIDVerticalSpeed, _currentBlendInput.y);
        _animator.SetFloat(_animIDInputMagnitude, inputTarget.magnitude);
    }
}
