using UnityEngine;

public class DeParent : MonoBehaviour
{
    void Awake()
    {
        transform.SetParent(null);
    }
}
