using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class ApplyGravAfterImpact : MonoBehaviour
{
    [SerializeField] Rigidbody body;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.layer = 0;
        body.useGravity = true;
        var d = collision.collider.GetComponent<Door>();
        if (d && d.state == DoorState.Closed)
        {
            d.OpenDoor();
            //d.Invoke("CloseDoor", 5.0f);
        }
        GameObject.Destroy(this);
    }
}
