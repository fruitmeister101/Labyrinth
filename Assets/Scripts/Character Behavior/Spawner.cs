using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject Prefab;
    [SerializeField] float Cooldown;
    float maxCooldown;
    private void Start()
    {
        maxCooldown = Cooldown;
        Cooldown = Random.Range(maxCooldown / 2, maxCooldown);
    }
    private void FixedUpdate()
    {
        Cooldown -= Time.fixedDeltaTime;
        if (Cooldown < 0)
        {
            Cooldown = Random.Range(maxCooldown / 2, maxCooldown);
            Instantiate(Prefab, transform.position, transform.rotation);
        }
    }
}
