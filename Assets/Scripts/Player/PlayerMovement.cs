using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _maxSpeed = 10.0f;
    [SerializeField] private float _minSpeed = 1.0f;
    [SerializeField] private float _jumpForce = 10.0f;

    [SerializeField] private int _layer;

    [SerializeField] private Camera _camera;

    private CharacterController _characterController;
    private bool _isGrounded = true;
    private float _smoothTime = 0.12f;
    private float _rotationVelocity;

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
    }

    private void FixedUpdate()
    {
        Move();
        Gravity();
    }

    private void Gravity()
    {
        throw new NotImplementedException();
    }

    private void Move()
    {
        if (!_isGrounded)
            return;

        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        var verticalSpeed = Mathf.Lerp(_characterController.velocity.y, _speed * input.y, _smoothTime);
        var horizontalSpeed = Mathf.Lerp(_characterController.velocity.x, _speed * input.x, _smoothTime);

        var direction = new Vector3(horizontalSpeed, 0.0f, verticalSpeed);
        _characterController.Move(direction.normalized * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == _layer)
        {
            Debug.Log("Kek");
            _isGrounded = true;
        }
    }
}
