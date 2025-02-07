using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerLocomotionInput))]
[DefaultExecutionOrder(-1)]
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] private float _acceleration = 4.0f;
    [SerializeField] private float _sprintAcceleration = 7.0f;
    [SerializeField] private float _maxSpeed = 10.0f;
    [SerializeField] private float _minSpeed = 2.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private float _drag = 5f;
    [SerializeField] private float _movingTreshold = 0.01f;


    [Header("Camera Setting")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _lookSenseH = 0.1f;
    [SerializeField] private float _lookSenseV = 0.1f;
    [SerializeField] private float _lookLimitV = 89f;

    [Header("Other")]
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private LayerMask _layer;

    private PlayerLocomotionInput _input;
    private PlayerState _state;

    private CharacterController _characterController;
    private PlayerAnimator _playerAnimator;
    private Quaternion _playerTargetRotation;
    private Vector2 _cameraRotation = Vector2.zero;

    private float _speed = 1.0f;
    #endregion

    public float Speed 
    { 
        get { return _speed; } 
        set 
        {
            _speed = Mathf.Clamp(value, _minSpeed, _maxSpeed); 
        } 
    }

    #region UnityFunctions
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _input = GetComponent<PlayerLocomotionInput>();
        _state = GetComponent<PlayerState>();
    }

    private void Update()
    {
        UpdateMovementState();
        Move();
    }
    #endregion
    private void UpdateMovementState()
    {
        bool isMovementInput = _input.MovementInput != Vector2.zero;
        bool isMovingLaterally = IsMovingLaterally();
        bool isSprinting = _input.IsSprinting && isMovementInput;

        PlayerMovementState lateralState = isSprinting ? PlayerMovementState.Sprinting :
                                           isMovingLaterally || isMovementInput ? PlayerMovementState.Walking : PlayerMovementState.Idling;

        _state.SetPlayerMovementState(lateralState);
    }

    private bool IsMovingLaterally()
    {
        Vector3 laterallyVelocity = new Vector3(_characterController.velocity.x, 0f, _characterController.velocity.z);

        Debug.Log(laterallyVelocity);
        return laterallyVelocity.magnitude > _movingTreshold;
    }

    private void Move()
    {
        float lateralAcceleration = _state.CurrentPlayerMovementState == PlayerMovementState.Sprinting ? _sprintAcceleration : _acceleration;
        var targetSpeed = _state.CurrentPlayerMovementState == PlayerMovementState.Sprinting ? _maxSpeed : _minSpeed;

        var cameraForwardXZ = new Vector3(_camera.transform.forward.x, 0f, _camera.transform.forward.z).normalized;
        var cameraRightXZ = new Vector3(_camera.transform.right.x, 0.0f, _camera.transform.right.z).normalized;
        Vector3 movementDirection = cameraRightXZ * _input.MovementInput.x + cameraForwardXZ * _input.MovementInput.y;

        Vector3 movementDelta = movementDirection * lateralAcceleration;
        Vector3 newVelocity = _characterController.velocity + movementDelta;

        Vector3 currentDrag = newVelocity.normalized * _drag;
        newVelocity = (newVelocity.magnitude > _drag) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(newVelocity, targetSpeed);
        _characterController.Move(newVelocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        _cameraRotation.x += _lookSenseH * _input.MouseInput.x;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - _lookSenseV * _input.MouseInput.y, -_lookLimitV, _lookLimitV);

        _playerTargetRotation.x += transform.eulerAngles.x + _lookSenseV * _input.MouseInput.x;
        transform.rotation = Quaternion.Euler(0.0f, _playerTargetRotation.x, 0.0f);

        _camera.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0.0f);
    }
}
