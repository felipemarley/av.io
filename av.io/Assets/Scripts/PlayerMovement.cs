using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isEngineOn = true;
    public float maxAcceleration = 10;
    public float currentAcceleration = 10;

    [SerializeField] private float drag = 0.95f;

    private float verticalInput;
    [SerializeField] private float rotationSpeedFactor = 5;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ToggleEngine("on");
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.E)) 
            ToggleEngine();
    }

    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();

        if (isEngineOn)
            ApplyDrag();
    }

    private void RotatePlayer()
    {
        float currentRotation = rb.rotation;
        float currentRotationSpeed = currentAcceleration * rotationSpeedFactor * Time.fixedDeltaTime;

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

    public void ToggleEngine(string on)
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

    private void ApplyDrag()
    {
        rb.linearVelocity *= drag;
    }

}
