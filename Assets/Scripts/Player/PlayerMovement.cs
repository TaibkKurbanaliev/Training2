using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _maxSpeed = 10.0f;
    [SerializeField] private float _minSpeed = 1.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private float _gravity = -9.8f;

    [SerializeField] private LayerMask _layer;

    [SerializeField] private Camera _camera;

    private CharacterController _characterController;
    private PlayerAnimator _playerAnimator;

    private bool _isGrounded = false;
    private float _smoothTime = 0.12f;
    private float _rotationVelocity;
    private float _verticalSpeed;

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
    }

    private void FixedUpdate()
    {
        Gravity();
        Move();
    }

    private void Gravity()
    {
        _verticalSpeed = Mathf.Clamp(_verticalSpeed + _gravity * Time.deltaTime, _gravity, _jumpForce);
    }

    private void Move()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        var forwardSpeed = Mathf.Lerp(_characterController.velocity.y, _speed * input.y, _smoothTime);
        var horizontalSpeed = Mathf.Lerp(_characterController.velocity.x, _speed * input.x, _smoothTime);

        var direction = new Vector3(horizontalSpeed, 0.0f, forwardSpeed);
        _characterController.Move(direction.normalized * Time.fixedDeltaTime + Vector3.up * _verticalSpeed);

        Debug.Log(_characterController.velocity);
        _playerAnimator.SetMovementSpeed(new Vector2(_characterController.velocity.x, _characterController.velocity.z));
    }
}
