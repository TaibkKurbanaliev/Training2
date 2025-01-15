using UnityEngine;

public class MysteryBox : Box
{
    protected override void Broke()
    {
        throw new System.NotImplementedException();
    }

    protected override void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
