using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;

    public static PlayerMovement Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Gets the rigid body of the player
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Horizontal input (Left and Right)
        vertical = Input.GetAxisRaw("Vertical"); // Vertical input (Up and Down)
    }
    
    private void FixedUpdate()
    {
        // Moves the player based on the input
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
