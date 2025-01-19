using System.Collections.Generic;
using UnityEngine;

public abstract class Box : MonoBehaviour, IDamagable
{
    [SerializeField] protected float Health = 100.0f; 
    [SerializeField] protected List<GameObject> PowerUps = new();
    public abstract void TakeDamage(float damage);
    protected abstract void Broke();
}
