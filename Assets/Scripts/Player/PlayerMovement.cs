using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _acceleration = 4.0f;
    [SerializeField] private float _maxSpeed = 10.0f;
    [SerializeField] private float _minSpeed = 2.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _drag = 5f;

    [SerializeField] private LayerMask _layer;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _lookSenseH = 0.01f;

    private CharacterController _characterController;
    private PlayerAnimator _playerAnimator;
    private InputSystem_Actions _inputActions;
    private Quaternion _playerTargetRotation;

    private float _speed = 1.0f;

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
        Move();
    }


    private void Move()
    {
        var input = _inputActions.Player.Move.ReadValue<Vector2>();
        var targetSpeed = _minSpeed;

        if (input == Vector2.zero) targetSpeed = 0.0f;

        var cameraForwardXZ = new Vector3(_camera.transform.forward.x, 0f, _camera.transform.forward.z);
        var cameraRightXZ = new Vector3(_camera.transform.right.x, 0.0f, _camera.transform.right.z);
        Vector3 movementDirection = cameraRightXZ * input.x + cameraForwardXZ * input.y;

        Vector3 movementDelta = movementDirection * _acceleration * Time.deltaTime;
        Vector3 newVelocity = _characterController.velocity + movementDelta;

        Vector3 currentDrag = newVelocity.normalized * _drag * Time.deltaTime;
        newVelocity = (newVelocity.magnitude > _drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
        _characterController.Move(newVelocity * Time.deltaTime);
        Debug.Log(_characterController.velocity);
    }

    private void LateUpdate()
    {
        _playerTargetRotation.x += transform.eulerAngles.x + _lookSenseH * _inputActions.Player.Look.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, _playerTargetRotation.x, 0f);
    }
}
