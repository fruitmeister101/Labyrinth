using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using UnityEngine.AI;
using UnityEditor.AI;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject LabyrinthController;
    [SerializeField] List<Door> Doors = new();
    [SerializeField] Transform walls;
    public Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
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
                door.OpenDoor();
            }
        }
    }
    public void Move(Vector3 destination)
    {
    }

}
