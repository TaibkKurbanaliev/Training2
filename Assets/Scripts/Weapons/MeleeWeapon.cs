using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private GameObject _owner;
    [SerializeField] private float _rotateSpeed = 5.0f;
    [SerializeField] private float _distanceFromOwner = 20.0f;
    protected override void Attack(IDamagable target)
    {
        target.TakeDamage(Damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable target))           
        {
            Attack(target);
        }
    }

    private void FixedUpdate()
    {
        Follow();
        transform.RotateAround( _owner.transform.localPosition + transform.right * _distanceFromOwner,
                                Vector3.up,
                                _rotateSpeed * Time.fixedDeltaTime);
    }

    private void Follow()
    {
        transform.position = _owner.transform.position;
    }
}
