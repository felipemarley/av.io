using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float verticalInput;
    [SerializeField] private float rotationFactor = 5;

    private bool isEngineOn = true;
    [SerializeField] private float maxAcceleration = 15;
    [SerializeField] private float currentAcceleration = 15;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ToggleEngine("on");
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.E)) {
            ToggleEngine();
        };
    }

    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
    }

    private void RotatePlayer()
    {
        float currentRotation = rb.rotation;
        float currentRotationSpeed = currentAcceleration * rotationFactor * Time.fixedDeltaTime;


        rb.MoveRotation(currentRotation + currentRotationSpeed * verticalInput);
    }

    private void MovePlayer()
    {

        Vector2 direction = transform.right.normalized;
        rb.AddForce(direction * currentAcceleration);
    }

    private void ToggleEngine()
    {
        if(isEngineOn)
        {
            ToggleEngine("off");
        } else
        {
            ToggleEngine("on");
        }
    }

    private void ToggleEngine(string on)
    {
        if(on.ToLower() == "on")
        {
            isEngineOn = true;
            rb.gravityScale = 0;
            currentAcceleration = maxAcceleration;
        } else 
        {
            isEngineOn = false;
            rb.gravityScale = 1;
            currentAcceleration = 0;
        }
    }
}
