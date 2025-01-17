using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float Damage;

    protected abstract void Attack(IDamagable target);

}
