using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicCharacter : MonoBehaviour
{
    protected Vector3 moveVec;
    [SerializeField] protected Rigidbody body;
    protected bool Jump = false;
    [Range(1, 25)] [SerializeField] protected int speed = 1;
    [Range(1, 25)] [SerializeField] protected int jumpHeight = 1;
    [SerializeField] protected NavMeshAgent nav;
    [SerializeField] Camera Cam;

    public bool CanUseItemClass;
    public List<Item> Guns;
    public List<Item> Items;
    public List<Item> Upgrades;
    public int GunLimit;
    public int ItemLimit;
    public int UpgradeLimit;

    public float Health;

    

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        moveVec = new Vector3();
        if (!body) body = GetComponent<Rigidbody>(); if (!body) throw new System.Exception($"{this}.Rigidbody Missing");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Jump = Input.GetKeyDown(KeyCode.Space)?true:Jump;
        //moveVec = new Vector3(Input.GetAxis("Horizontal"), body.velocity.y / speed * 4.0f, Input.GetAxis("Vertical"));
        
    }
    protected virtual void FixedUpdate()
    {

        //if (ShouldParent)
        //{
        //    transform.parent = P.transform;
        //}
<<<<<<< HEAD
        CheckHeight();
    }
    protected virtual void CheckHeight()
    {
=======
>>>>>>> parent of 518d20d (Fixed some stuff and changed a few others)
        if (transform.position.y < -10)
        {
            GameObject.Destroy(gameObject);
        }
    }
    public bool Equip(Item item)
    {
        switch (item.Type)
        {
            case ItemType.Gun:
                if (Guns.Count < GunLimit)
                {
                    Guns.Add(item);
                    return true;
                }
                break;
            case ItemType.Item:
                if (Items.Count < ItemLimit)
                {
                    Items.Add(item);
                    return true;
                }
                break;
            case ItemType.Upgrade:
                if (Upgrades.Count < UpgradeLimit)
                {
                    Upgrades.Add(item);
                    return true;
                }
                break;
            case ItemType.Ammo:
                break;
            case ItemType.Instant:
                break;
            default:
                break;
        }
        return false;
    }

    protected void ShootGunToMouse(int gun)
    {
        Guns[gun].Shoot(transform.position, Cam.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, Cam.transform.position.y - transform.position.y)), Guns[gun].DefaultBullet, 15.0f, 1.15f);
        //Debug.Log(Cam.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, -30.0f)));
    }
<<<<<<< HEAD
    public void CheckHealth()
=======

    protected virtual void OnCollisionEnter(Collision collision)
>>>>>>> parent of 518d20d (Fixed some stuff and changed a few others)
    {
        if (Health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
<<<<<<< HEAD
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
=======
>>>>>>> parent of 518d20d (Fixed some stuff and changed a few others)
        //Debug.Log(collision.collider);
        var d = collision.collider.GetComponent<Door>();
        if (d && d.state == DoorState.Closed)
        {
            d.OpenDoor();
            d.Invoke("CloseDoor", 5.0f);
        }
    }
}
