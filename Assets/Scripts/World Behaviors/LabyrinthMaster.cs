using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class LabyrinthMaster : MonoBehaviour
{
    [SerializeField] public int countX;
    [SerializeField] public int countZ;
    [SerializeField] public int Spacing;
    [SerializeField] public int UpdateInterval;
    [SerializeField] List<GameObject> Labyrinth;
    [SerializeField] public static LabyrinthMaster MasterReference;
    public GameObject Player;
    int tempCounter = 0;
    bool isMoving = false;

    [SerializeField] GameObject PrefabRoom;
    // Start is called before the first frame update
    void Awake()
    {
        if (!LabyrinthMaster.MasterReference) LabyrinthMaster.MasterReference = this; else return;

        int posX = -countX / 2;
        for (int x = 0; x < countX; x++)
        {
            int posY = -countZ / 2;
            for (int y = 0; y < countZ; y++)
            {
                var newThing = Instantiate(PrefabRoom, new Vector3(x * Spacing, 0, y * Spacing), Quaternion.Euler(0, 0, 0), transform);
                Labyrinth.Add(newThing);
                posY++;
            }
            posX++;
        }
    }

    public void MoveLabyrinth(GameObject obj, int dir)
    {
        if (isMoving) return;
        isMoving = true;
        dir %= 4;
        Tile[] ToBeMoved;
        if (dir == 1 || dir == 3)
        {
            dir = dir - 2;
            ToBeMoved = (from i in Labyrinth where i.transform.position.x == obj.transform.position.x select i.GetComponent<Tile>()).ToArray();
            foreach (var item in ToBeMoved)
            { // Moves All in Collumn and wraps around
                // var goTo = new Vector3(item.transform.position.x, 0, (((item.transform.position.z / Spacing + dir + countZ)) % countZ) * Spacing);
                var goTo = new Vector3(item.transform.position.x, 0, item.transform.position.z + Spacing * dir);
                item.Move(goTo);
            }
        }
        else if (dir == 0 || dir == 2)
        {
            dir = dir - 1;
            ToBeMoved = (from i in Labyrinth where i.transform.position.z == obj.transform.position.z select i.GetComponent<Tile>()).ToArray();
            foreach (var item in ToBeMoved)
            { // Moves All in Row and wraps around
                // var goTo = new Vector3((((item.transform.position.x / Spacing + dir + countX)) % countX) * Spacing,0,item.transform.position.z);
                var goTo = new Vector3(item.transform.position.x + Spacing * dir, 0, item.transform.position.z);
                item.Move(goTo);
            }
        }
        

    }

    public static void RelinquishControl()
    {
        MasterReference.isMoving = false;
    }
    private void FixedUpdate()
    {
        tempCounter++;

        if (tempCounter > UpdateInterval)
        {
            tempCounter = 0;
            var temp = Labyrinth[Random.Range(0, Labyrinth.Count)];
            MoveLabyrinth(temp, Random.Range(0,4));
        }
    }

}
