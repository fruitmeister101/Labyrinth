using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicCharacter
{
    [SerializeField] float shootDelay;
    float cd = 0;
    protected override void Awake()
    {
        base.Awake();
        nav.updateRotation = false;
        nav.updatePosition = true;
        nav.updateUpAxis = false;
    }
    protected override void Update()
    {
        Jump = Input.GetKey(KeyCode.Space);
        moveVec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (cd <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                ShootGunToMouse(0);
                cd += shootDelay;
            }
        }
        else
        {
            cd -= Time.deltaTime;
        }
        base.Update();
    }

    public override void CheckFall(float Height = -10)
    {
        if (transform.position.y < -10)
        {
            transform.position = transform.parent.position;
        }
    }

    protected override void FixedUpdate()
    {
        var grounded = Physics.Raycast(transform.position, Vector3.down, 1.25f);
        //RaycastHit P;
        //Ray r = new(transform.position, Vector3.down);
        //var ShouldParent = Physics.Raycast(r, out P);
        var calcSpeed = speed / 4.0f;
        if (grounded)
        {
            //body.velocity += moveVec * calcSpeed;

            if (Jump)
            {
                //body.velocity = new(body.velocity.x, jumpHeight, body.velocity.z);
                body.velocity = new(moveVec.x * speed, jumpHeight, moveVec.z * speed);
            }
            else body.AddForce(moveVec.x * speed, body.velocity.y, moveVec.z * speed);
        }
        base.FixedUpdate();
    }


    protected override void CheckHeight()
    {
        if (transform.position.y < -10)
        {
            transform.position = transform.parent.position;
        }
    }



}
