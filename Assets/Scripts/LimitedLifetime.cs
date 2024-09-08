using UnityEngine;

public class LimitedLifetime : MonoBehaviour
{
    [SerializeField] float Lifetime;


    private void FixedUpdate()
    {
        Lifetime -= Time.fixedDeltaTime;
        if (Lifetime < 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
