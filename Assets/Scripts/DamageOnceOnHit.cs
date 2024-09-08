using UnityEngine;

public class DamageOnceOnHit : DamageOnHit
{
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        GameObject.Destroy(this);
    }
}
