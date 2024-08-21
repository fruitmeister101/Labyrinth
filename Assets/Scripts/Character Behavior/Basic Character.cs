using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicCharacter : MonoBehaviour
{
    protected Vector3 moveVec;
    protected Rigidbody body;
    protected bool Jump = false;
    [Range(1, 25)] [SerializeField] protected int speed = 1;
    [Range(1, 25)] [SerializeField] protected int jumpHeight = 1;
    [SerializeField] protected NavMeshAgent nav;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        moveVec = new Vector3();
        body = GetComponent<Rigidbody>(); if (!body) throw new System.Exception($"{this}.Rigidbody Missing");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Jump = Input.GetKeyDown(KeyCode.Space)?true:Jump;
        Jump = Input.GetKey(KeyCode.Space);
        //moveVec = new Vector3(Input.GetAxis("Horizontal"), body.velocity.y / speed * 4.0f, Input.GetAxis("Vertical"));
        moveVec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

    }
    protected virtual void FixedUpdate()
    {
        var grounded = Physics.Raycast(transform.position, Vector3.down, 1.15f);
        var calcSpeed = speed / 4.0f;
        if (grounded)
        {
            body.velocity += moveVec * calcSpeed;

            if (Jump)
            {
                body.velocity = new(body.velocity.x, jumpHeight, body.velocity.z);
            }
        }
        if (body.velocity.magnitude - Mathf.Abs(body.velocity.y) > calcSpeed)
        {
            var norm = body.velocity.normalized;
            body.velocity = new(norm.x * calcSpeed, body.velocity.y, norm.z * calcSpeed);
        }
    }
}
