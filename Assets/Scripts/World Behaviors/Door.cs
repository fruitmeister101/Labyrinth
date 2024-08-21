using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{

    [SerializeField] Material openedDoor;
    [SerializeField] Material closedDoor;
    [SerializeField] Material blockedDoor;
    NavMeshObstacle obs;
    MeshRenderer rend;
    Collider col;
    DoorState state = DoorState.Open;

    private void Awake()
    {
        if (!obs) obs = GetComponent<NavMeshObstacle>();
        if (!openedDoor) throw new System.Exception($"{this}.OpenedDoor Material is null");
        if (!closedDoor) throw new System.Exception($"{this}.ClosedDoor Material is null");
        rend = GetComponent<MeshRenderer>();
        if (!rend) throw new System.Exception($"{this}.rend is null");
        col = GetComponent<Collider>();
        if (!col) throw new System.Exception($"{this}.col is null");
    }

    public void OpenDoor()
    {
        //rend.material = openedDoor;
        transform.position = new(transform.position.x, -5.0f, transform.position.z);
        state = DoorState.Open;
    }
    public void CloseDoor(bool cleared = false)
    {
        rend.material = cleared ? openedDoor : closedDoor;
        if (obs && cleared) obs.enabled = false;
        transform.position = new(transform.position.x, 5, transform.position.z);
        state = DoorState.Closed;
    }
    public void CloseDoor() { CloseDoor(true); }
    public void BlockDoor()
    {
        rend.material = blockedDoor;
        transform.position = new(transform.position.x, 5, transform.position.z);
        state = DoorState.Blocked;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == DoorState.Closed)
        {
            OpenDoor();
            Invoke("CloseDoor", 5.0f);
        }
    }

}
public enum DoorState
{
    Open,
    Closed,
    Blocked
}