using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEditor.AI;
using System.Threading.Tasks;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject LabyrinthController;
    [SerializeField] List<Door> Doors = new();
    [SerializeField] Transform walls;
    public Rigidbody body;
    public NavMeshPath pathToPlayer = new NavMeshPath();
    public GameObject player;

    Vector3 startLoc;
    Vector3 endLoc;
    Vector3 dif;
    bool notDone = false;
    float interp = 1.0f;
    [Range(0.0f,1.0f)] public float interpSpeed;
    //RigidbodyConstraints DefaultConstraints;

    private void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody>();
        //DefaultConstraints = body.constraints;
        //body.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Start()
    {
        //if (!Mesh) throw new System.Exception($"No Labyrinth Controller on {gameObject}");
        var count = 0;
        foreach (var door in Doors)
        {
            if (count < 2 && Random.Range(0,3) == 0)
            {
                door.BlockDoor();
                count++;
            }
            else
            {
                door.CloseDoor(true);
            }
        }
        startLoc = transform.position;
        endLoc = transform.position;


    }
    private void FixedUpdate()
    {
        if (interp < 1)
        {
            interp += interpSpeed;
            Mathf.Clamp01(interp);
            body.velocity = dif / 4.0f;
            body.MovePosition(Vector3.Lerp(startLoc, endLoc, interp));
        }
        else if (notDone)
        {
            body.velocity = Vector3.zero;
            body.MovePosition(endLoc);
            notDone = false;
            var T = transform.position;
            var M = LabyrinthMaster.MasterReference;
            if (T.x < -M.Spacing / 2)
            {
                endLoc = new(M.countX * (M.Spacing) - M.Spacing, 0, T.z);
            }
            else if (T.x > M.countX * (M.Spacing - 1))
            {
                endLoc = new(0, 0, T.z);
            }
            else if (T.z < -M.Spacing / 2)
            {
                endLoc = new(T.x, 0, M.countZ * (M.Spacing) - M.Spacing);
            }
            else if (T.z > M.countZ * (M.Spacing - 1))
            {
                endLoc = new(T.x, 0, 0);
            }
            //body.MovePosition(endLoc);
            //body.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = endLoc;
            startLoc = endLoc;
            body.velocity = Vector3.zero;
        }
        else
        {
            body.MovePosition(endLoc);
            transform.position = endLoc;
        }

        if (GetComponentInChildren<Player>() || Random.Range(0,100) == 0)
        {
            DoPathing();
        }

    }
    public void Move(Vector3 destination)
    {

        //body.constraints = DefaultConstraints;
        endLoc = destination;
        interp = 0.0f;
        dif = endLoc - startLoc;
        notDone = true;
    }
    void DoPathing()
    {
        if (pathToPlayer is null) 
        { 
            pathToPlayer = new();
        } 
        NavMesh.CalculatePath(transform.position, player.transform.position, NavMesh.AllAreas, pathToPlayer);

    }

    private void OnTriggerEnter(Collider other)
    {
        DoPathing();
        other.transform.parent = transform;
    }
}
