using UnityEngine;

public class SpeedBooster : PowerUp
{
    [SerializeField] private float _speedBoostValue = 5.0f;
    protected override void Apply(GameObject target)
    {
        if (target.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.Speed = _speedBoostValue;
        }
    }
}
