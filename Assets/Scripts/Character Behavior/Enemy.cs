using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEditor.AI;

public class Enemy : BasicCharacter
{
    [SerializeField] protected GameObject Player;
    [SerializeField] Vector3[] corners;

    Vector3 Target = new();
    float timer = 0;
    
    

    override protected void Awake()
    {
        base.Awake();
        if (!Player) Player = GameObject.FindGameObjectWithTag("Player");
        if (!Player) throw new System.Exception($"{gameObject} does not have target");
        if (!nav) nav = GetComponent<NavMeshAgent>();
        if (!nav) throw new System.Exception($"{gameObject} does not have NavMesh Agent");
        nav.updateRotation = true;
        nav.updatePosition = true;

    }

    protected override void FixedUpdate()
    {
        if (timer <= 0)
        //if (nav.isStopped)
        {
            Target = Player.transform.position;
            nav.SetDestination(Target);
            timer = Mathf.Min( 10.0f, (transform.position - Target).magnitude / 10.0f);
        }
        else if (!nav.pathPending)
        {
            timer -= Time.deltaTime;
            corners = nav.path.corners;
        }


        
        
    }
}
