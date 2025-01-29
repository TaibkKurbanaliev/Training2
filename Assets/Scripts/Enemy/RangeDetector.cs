using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 3.0f;
    [SerializeField] private float _maxDistance = 10.0f;
    [SerializeField] private LayerMask _target;

    private RaycastHit _hit;
    
    private void Update()
    {
        if (Physics.SphereCast(transform.position, _radius, transform.forward, out _hit, _maxDistance, _target))
        {
            ;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.forward * _maxDistance, _radius);
    }
}
