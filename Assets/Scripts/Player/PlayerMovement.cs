using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _acceleration = 4.0f;
    [SerializeField] private float _deceleration = 2.0f;
    [SerializeField] private float _maxSpeed = 10.0f;
    [SerializeField] private float _minSpeed = 1.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private float _gravity = -9.8f;

    [SerializeField] private LayerMask _layer;

    [SerializeField] private Camera _camera;

    private CharacterController _characterController;
    private PlayerAnimator _playerAnimator;

    private bool _isGrounded = false;
    private float _smoothTime = 0.012f;
    private float _rotationVelocity;
    private float _verticalSpeed;
    private InputSystem_Actions _inputActions;
    private Vector2 _currentVelocity;

    public float Speed 
    { 
        get { return _speed; } 
        set 
        {
            _speed = Mathf.Clamp(value, _minSpeed, _maxSpeed); 
        } 
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();
    }

    private void Update()
    {
        Gravity();
        Move();
    }

    private void Gravity()
    {
        //_verticalSpeed = Mathf.Clamp(_verticalSpeed + _gravity * Time.deltaTime, _gravity, _jumpForce);
    }

    private void Move()
    {
        var input = _inputActions.Player.Move.ReadValue<Vector2>();
        var targetDir = new Vector3(input.x, 0.0f, input.y);

        if (input.magnitude > 0.0f)
        {
            _currentVelocity = Vector2.Lerp(_currentVelocity, input * _speed, _acceleration * Time.deltaTime);
        }
        else
        {
            _currentVelocity = Vector2.Lerp(_currentVelocity, input * _speed, _deceleration * Time.deltaTime);
        }

        _characterController.Move(new Vector3(_currentVelocity.x, 0.0f, _currentVelocity.y) * Time.deltaTime + Vector3.down * Time.deltaTime);

        Debug.Log(_characterController.velocity);
        _playerAnimator.SetMovementSpeed(new Vector2(_characterController.velocity.x, _characterController.velocity.z));
    }
}
