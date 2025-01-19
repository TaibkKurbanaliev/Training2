using UnityEngine;

public class MysteryBox : Box
{
    public override void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
            Broke();
    }

    protected override void Broke()
    {
        Destroy(gameObject);
    }

}
