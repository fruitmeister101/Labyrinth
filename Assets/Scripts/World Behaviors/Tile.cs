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
    [SerializeField] public List<Door> Doors = new();
    public int Rotation = 0;
    //[SerializeField] Transform walls;
    public Rigidbody body;
    //public NavMeshPath pathToPlayer;
    public int DistToPlayer = 0;
    public GameObject player;
    [SerializeField] Tile[] Neighbors;
    public Tile Closest;

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
            if (count < 2 && Random.Range(0,4) == 0)
            {
                door.BlockDoor();
                //door.CloseDoor();
                count++;
            }
            else
            {
                door.CloseDoor();
            }
        }
        startLoc = transform.position;
        endLoc = transform.position;


    }
    private void FixedUpdate()
    {
        DoingMoving();





        if (GetComponentInChildren<Player>() /*|| Random.Range(0,100) == 0*/)
        {
            //DoPathing();
            //GetNeighbors();
            DistToPlayer = 0;
        }
        else
        {
            DoPathing();
        }
        if (!Closest)
        {
            Closest = this;
        }
        foreach (var c in Neighbors)
        {
            if (c && c.DistToPlayer < Closest.DistToPlayer)
            {
                Closest = c;
            }
        }
        

    }
    void DoingMoving()
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
            LabyrinthMaster.RelinquishControl();
        }
        else
        {
            body.MovePosition(endLoc);
            transform.position = endLoc;
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
        /*if (pathToPlayer is null) 
        { 
            pathToPlayer = new();
        } 
        NavMesh.CalculatePath(transform.position, player.transform.position, NavMesh.AllAreas, pathToPlayer);
        */

        int BoardSize = LabyrinthMaster.MasterReference.countX * LabyrinthMaster.MasterReference.countZ;
        int min = BoardSize*2;
        for (int i = 0; i < 4; i++)
        {
            if (Neighbors[i] is null) 
            {
                continue;
            } 
            else if (Doors[i].state != DoorState.Blocked && Neighbors[i].Doors[(i + 2) % 4].state != DoorState.Blocked)
            {
                min = Mathf.Min(min, Neighbors[i].DistToPlayer, DistToPlayer);
            }
        }
        DistToPlayer = min + 1;
    }

    public void GetNeighbors()
    {
        Closest = this;
        var MR = LabyrinthMaster.MasterReference;
        var neighbors = (from t in MR.Labyrinth
                        where (
                            t.transform.position == transform.position + new Vector3(MR.Spacing,0,0)
                        ||  t.transform.position == transform.position + new Vector3(0, 0, MR.Spacing)
                        ||  t.transform.position == transform.position + new Vector3(-MR.Spacing, 0, 0)
                        || t.transform.position == transform.position + new Vector3(0, 0, -MR.Spacing)
                        )
                        select t.GetComponent<Tile>()).ToList();
        Neighbors = new Tile[4];
        while (neighbors.Count > 0)
        {
            if (neighbors[0].transform.position == transform.position + new Vector3(0, 0, MR.Spacing))
            {
                Neighbors[0] = neighbors[0];
            }
            else if (neighbors[0].transform.position == transform.position + new Vector3(MR.Spacing, 0, 0))
            {
                Neighbors[1] = neighbors[0];
            }
            else if (neighbors[0].transform.position == transform.position + new Vector3(0, 0, -MR.Spacing))
            {
                Neighbors[2] = neighbors[0];
            }
            else if (neighbors[0].transform.position == transform.position + new Vector3(-MR.Spacing, 0, 0))
            {
                Neighbors[3] = neighbors[0];
            }
            neighbors.RemoveAt(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DoPathing();
        other.transform.parent = transform;
    }
}
