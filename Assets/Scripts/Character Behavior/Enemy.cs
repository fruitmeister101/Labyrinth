using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        nav.updateUpAxis = true;

    }
    private void Start()
    {
        if (!Player)
        {
            Player = LabyrinthMaster.MasterReference.Player;
        }
    }

    protected override void FixedUpdate()
    {
        if (timer <= 0)
        //if (nav.isStopped)
        {
            Target = Player.transform.position;
            nav.SetDestination(Target);
            //nav.nextPosition = body.position;
            timer = (nav.nextPosition - Target).magnitude / 10.0f;
        }
        else if (!nav.pathPending)
        {
            timer -= Time.deltaTime;
            corners = nav.path.corners;
        }

        /*
        if ((body.position - nav.nextPosition).magnitude > 3)
        {
            nav.nextPosition = body.position;
            //nav.nextPosition = (nav.nextPosition - body.position) * 0.9f + body.position;
        }
        
        body.AddForce ((nav.nextPosition - transform.position) * speed - body.velocity * 5);
        */


        base.FixedUpdate();
    }
}
