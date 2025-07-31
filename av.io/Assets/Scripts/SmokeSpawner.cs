using UnityEngine;

public class SmokeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private GameObject smokeTrail;
    [SerializeField] private float cooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnSmoke), 0f, cooldown);
    }

    void SpawnSmoke()
    {
        GameObject smokeInstance = Instantiate(smokePrefab, transform.position, Quaternion.identity);
        smokeInstance.transform.parent = smokeTrail.transform;
    }
}
