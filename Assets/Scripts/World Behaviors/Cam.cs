using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField]Transform follow;
    Vector3 relative;
    float size;
    int spacing;
    void Start()
    {
        relative = follow.position - transform.position;
        size = LabyrinthMaster.MasterReference.Spacing;
        spacing = LabyrinthMaster.MasterReference.Spacing;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp( transform.position, new Vector3(Mathf.Floor((follow.position.x + 13.5f) / spacing) * size - relative.x, transform.position.y, Mathf.Floor((follow.position.z + 13.5f) / spacing) * size - relative.z), 0.05f);
    }
}
