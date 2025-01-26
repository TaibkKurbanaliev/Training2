using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private int _money;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private Weapon _weapon;

    public bool IsAlive { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            ErnMoney(coin.Value);
            Destroy(coin.gameObject);
        }
        else if (collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
        }
    }

    private void ErnMoney(int money)
    {
        _money = money;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
}
