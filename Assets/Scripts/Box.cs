using System.Collections.Generic;
using UnityEngine;

public abstract class Box : MonoBehaviour
{
    [SerializeField] protected float Health = 100.0f; 
    [SerializeField] protected List<PowerUp> PowerUps = new();
    protected abstract void Broke();
    protected abstract void TakeDamage(float damage);

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage();
    }
}
