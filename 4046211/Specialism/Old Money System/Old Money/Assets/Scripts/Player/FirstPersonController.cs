using System;
using System.Collections;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public bool CanMove = true; // Checks if the player can move
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey); // Checks if the player is sprinting
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded; // Checks if the player should jump

    [Header("Functional Options")]
    public bool canLook = true; // Determines whether the player can look around
    [SerializeField] private bool canSprint = true; // Determines whether the player can sprint
    [SerializeField] private bool canJump = true; // Determines whether the player can jump
    public bool canInteract = true; // Determines whether the player can interact with objects

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift; // Sprint key
    [SerializeField] private KeyCode jumpKey = KeyCode.Space; // Jump key
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0; // Interact key 
    [SerializeField] private KeyCode altInteractKey = KeyCode.Mouse1; // Alternate Interact key

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 5.0f; // Walking speed
    [SerializeField] private float sprintSpeed = 8.0f; // Sprinting speed

    [Header("Look Parameters")]
    [SerializeField, Range(1,10)] private float lookSpeedX = 2.0f; // Speed at which the camera turns on the x axis
    [SerializeField, Range(1,10)] private float lookSpeedY = 2.0f; // Speed at which the camera turns on the y axis
    [SerializeField, Range(1,180)] private float upperLookLimit = 80.0f; // How high the camera rotates up
    [SerializeField, Range(1,180)] private float lowerLookLimit = 80.0f; // How low the camera rotates down

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;  // Default force to apply when jumping
    [SerializeField] private float gravity = 30.0f;  // Default gravity value

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;

    private Camera playerCamera;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    public static FirstPersonController instance;

    void Awake() // On awake
    {
        instance = this;
        playerCamera = GetComponentInChildren<Camera>();  // Set player camera variable to the camera
        characterController = GetComponent<CharacterController>();  // Set character controller variable to the character controller
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the screen
        Cursor.visible = false;  // Disable view of cursor on screen
    }

    // Every frame
    void Update()
    {   
        // if ???
        if (CanMove)
        {
            // Function to handle players movement
            HandleMovementInput();

            // If looking around is enabled
            if(canLook)
                HandleMouseLock();  // Call function to handle players mouse movement
            else
            {
                Cursor.lockState = CursorLockMode.None;  // Lock the cursor to the screen
                Cursor.visible = true;  // Disable view of cursor on screen
            }

            // If jumping is enabled
            if(canJump)
                HandleJump();  // Call function to handle players jumping

            // Call function to apply the movements made in that frame
            ApplyFinalMovements();
        }

        if (canInteract)
        {
            HandleInteractionCheck();
            HandleInteractionInput();
        }
    }

    private void HandleMovementInput()
    {   
        // set currentInput variable to verctor 2 sprint/walk speed * vertical axis, sprint/walk speed * horizontal axis. Which speed depends on if the user is holding the sprint key or not.
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;  // Create a float variable of the move direction of the users y coordinates

        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLock()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the screen
        Cursor.visible = false;  // Disable view of cursor on screen
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        // If the user should jump lambda function line 10
        if(ShouldJump)
            moveDirection.y = jumpForce;  // Set the y axis direction to the agreed force the user should jump
    }

    private void HandleInteractionCheck()
    {
        var ray = playerCamera.ViewportPointToRay(interactionRayPoint);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

        if(Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance) && hit.collider.gameObject.layer == 6)
        {
            if(currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID())
            {
                hit.collider.TryGetComponent(out currentInteractable);
                
                if(currentInteractable)
                    currentInteractable.OnFocus();
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractionInput()
    {
        if (currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            if (Input.GetKeyDown(interactKey)) currentInteractable.OnInteract();

            if(Input.GetKeyDown(altInteractKey)) currentInteractable.OnAltInteract();
        }
    }

    private void ApplyFinalMovements()
    {   
          if (!characterController.isGrounded)
        {
            // Starts moving the character down if he is in the air
            moveDirection.y -= gravity * Time.deltaTime;

            // If player hits the ceiling
            if(characterController.collisionFlags == CollisionFlags.Above)
            {
                // Stop adding velocity to the character
                moveDirection = Vector3.zero;
                // This prevents the player from sticking
                // to the ceiling and moving along it
                characterController.stepOffset = 0;
            }
        }
        // When the player is actually grounded
        else
        {
            if(characterController.stepOffset == 0)
            {
                // Resets the stepOffset to the default float so
                // the character is still able to function correctly
                // while walking up steps or across bumps.
                characterController.stepOffset = 0.3f;
            }
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }
}