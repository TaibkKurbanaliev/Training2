using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private int _layer;

    private Rigidbody _rb;
    private bool _isGrounded = true;

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
        _rb.AddForce(direction.normalized * _speed, ForceMode.Force);

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnterw(Collision collision)
    {
        
        if (collision.collider.gameObject.layer == _layer)
        {
            Debug.Log("Kek");
            _isGrounded = true;
        }
    }
}
