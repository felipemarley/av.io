using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void LateUpdate()
    {
        Vector3 targetPosition = target.transform.position;
        transform.position = new(targetPosition.x, targetPosition.y, -10);
    }
}
