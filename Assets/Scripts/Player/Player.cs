using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private int _money;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private Weapon _weapon;

    public bool IsAlive { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
        {
            ErnMoney(coin.Value);
            Destroy(coin.gameObject);
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
