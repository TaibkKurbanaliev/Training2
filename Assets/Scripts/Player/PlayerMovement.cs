using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _maxSpeed = 10.0f;
    [SerializeField] private float _minSpeed = 1.0f;
    [SerializeField] private float _jumpForce = 10.0f;

    [SerializeField] private int _layer;

    [SerializeField] private Camera _camera;

    private Rigidbody _rb;
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
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        var targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                  _camera.transform.eulerAngles.y;
        
        var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity,
                   _smoothTime);
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotation, transform.rotation.z);
        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        _rb.AddForce(targetDirection * _speed, ForceMode.Force);

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }

        CharacterController controller = GetComponent<CharacterController>();
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
