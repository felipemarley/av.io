using UnityEngine;

public class SmokeSpawner : MonoBehaviour
{
    public static SmokeSpawner instance;

    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private GameObject smokeTrail;
    [SerializeField] private float cooldown;

    private void Awake()
    {
        instance = this;
    }

    void SpawnSmoke()
    {
        GameObject smokeInstance = Instantiate(smokePrefab, transform.position, Quaternion.identity);
        smokeInstance.transform.parent = smokeTrail.transform;
    }

    public void TurnOnSmoke()
    {
        InvokeRepeating(nameof(SpawnSmoke), 0f, cooldown);
    }
    public void TurnOffSmoke()
    {
        CancelInvoke(nameof(SpawnSmoke));
    }
}
