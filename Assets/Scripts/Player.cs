using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _money;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            ErnMoney(coin.Value);
            Destroy(coin.gameObject);
        }
    }

    private void ErnMoney(int money)
    {
        _money = money;
    }
}
