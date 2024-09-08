using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public GameObject DefaultBullet;
    public ItemType Type;






    public void Shoot(Vector3 StartPos, Vector3 TargetPos, GameObject Bullet, float Velocity, float ForwardOffset)
    {
        var dir = (TargetPos - StartPos).normalized;
        var bullet = Instantiate(Bullet, StartPos + ForwardOffset * dir, Quaternion.identity);
        bullet.SetActive(true);
        bullet.transform.up = dir;
        var bBody = bullet.GetComponent<Rigidbody>();
        if (bBody) bBody.velocity = dir * Velocity;
    }



    private void OnTriggerEnter(Collider other)
    {
        var bc = other.GetComponent<BasicCharacter>();
        if (bc!.CanUseItemClass)
        {
            bc.Equip(this);
        }
    }


}
public enum ItemType
{
    Gun,
    Item,
    Upgrade,
    Ammo,
    Instant
}
