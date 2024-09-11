using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy2 : BasicCharacter
{
    [SerializeField] Transform parent;
    [SerializeField] NavMeshPath path;
    [SerializeField] float distToTarget;
    [SerializeField] int pathProgress;

    protected override void Awake()
    {
        base.Awake();
    }

    
    protected override void FixedUpdate()
    {
            //bool skip = false;
            /*if (parent)
            {
<<<<<<< HEAD
                var temp = parent.GetComponent<Tile>();
                if (temp is not null)
=======
                var dist = (path.corners[pathProgress] - transform.position);
                //moveVec = dist.normalized * speed;
                moveVec = new(dist.normalized.x * speed, body.velocity.y, dist.normalized.z * speed);
                transform.forward = new(dist.x, 0.01f, dist.z);
                if (dist.magnitude <= distToTarget)
>>>>>>> parent of 518d20d (Fixed some stuff and changed a few others)
                {
                    path = temp.pathToPlayer;
                    if ((transform.position - parent.position).magnitude < 10)
                    {
                        moveVec = (temp.player.transform.position - transform.position).normalized * speed;
                        transform.forward = new(moveVec.x, 0, moveVec.z);
                        skip = true;
                    }
                }
            }*/
        if (/*!skip &&*/ parent == transform.parent && path is not null && path.corners.Length > pathProgress)
        {
            var dist = (path.corners[pathProgress] - transform.position);
            //moveVec = dist.normalized * speed;
            moveVec = new(dist.normalized.x * speed, body.velocity.y, dist.normalized.z * speed);
            transform.forward = new(dist.x, 0, dist.z);
            if (dist.magnitude <= distToTarget)
            {
                pathProgress = Mathf.Min(pathProgress + 1, path.corners.Length - 1);
            }
        }
        else /*if (!skip)*/
        {
            parent = transform.parent;
            pathProgress = 1;
        }
        var temp = parent.GetComponent<Tile>();
        if (temp is not null)
        {
            path = temp.pathToPlayer;
        }
        base.FixedUpdate();
        body.AddForce (moveVec);
    }
}
