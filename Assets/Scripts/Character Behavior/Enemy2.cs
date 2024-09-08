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
        if (parent == transform.parent)
        {
            
            if (path is not null && path.corners.Length > pathProgress)
            {
                var dist = (path.corners[pathProgress] - transform.position);
                //moveVec = dist.normalized * speed;
                moveVec = new(dist.normalized.x * speed, body.velocity.y, dist.normalized.z * speed);
                transform.forward = new(dist.x, 0.01f, dist.z);
                if (dist.magnitude <= distToTarget)
                {
                    pathProgress = Mathf.Min(pathProgress + 1, path.corners.Length - 1);
                }
            }
        }
        else
        {
            parent = transform.parent;
            pathProgress = 1;
            path = parent.GetComponent<Tile>().pathToPlayer;
        }
        base.FixedUpdate();
        body.AddForce (moveVec);
    }
}
