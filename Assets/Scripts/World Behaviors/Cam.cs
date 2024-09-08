using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]

public class Cam : MonoBehaviour
{
    [SerializeField]Transform follow;
    [SerializeField] Camera cam;
    Vector3 relative;
    float size;
    int spacing;
    Vector3 relPoint;
    Vector3 mousePoint;
    void Start()
    {
        //relative = follow.position - transform.position;
        relative = transform.position - follow.position;
        size = LabyrinthMaster.MasterReference.Spacing;
        spacing = LabyrinthMaster.MasterReference.Spacing;
        cam = GetComponent<Camera>();
    }

    /*void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Floor((follow.position.x + 13.5f) / spacing) * size - relative.x, transform.position.y, Mathf.Floor((follow.position.z + 13.5f) / spacing) * size - relative.z), 0.05f);
    }*/
    void FixedUpdate()
    {
        relPoint = new(follow.position.x, relative.y, follow.position.z);
        mousePoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, relative.y));
        transform.position = Vector3.Lerp(transform.position, relPoint, 0.1f);
        //transform.position = Vector3.Lerp( transform.position, new(follow.position.x, relative.y, follow.position.z), 0.01f);
    }
}
