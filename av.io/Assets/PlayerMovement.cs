using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float verticalInput;
    [SerializeField] private float rotationSpeed = 30;
    [SerializeField] private float acceleration = 15;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
    }

    private void RotatePlayer()
    {
        float currentRotation = rb.rotation;
        rb.MoveRotation(currentRotation + rotationSpeed * verticalInput * Time.fixedDeltaTime);
    }

    private void MovePlayer()
    {
        Vector2 direction = transform.right.normalized;
        rb.AddForce(direction * acceleration);
    }
}
