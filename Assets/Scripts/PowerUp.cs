using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    protected abstract void Apply(GameObject target);

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Apply(other.gameObject);
        Destroy(gameObject);
    }
}
