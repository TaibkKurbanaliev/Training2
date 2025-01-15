using UnityEngine;

public class MeleeWeapon : Weapon
{
    protected override void Attack(IDamagable target)
    {
        target.TakeDamage(Damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable target))
        {
            Attack(target);
        }
    }
}
