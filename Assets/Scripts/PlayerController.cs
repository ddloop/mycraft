using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lookSpeed = 5f;
    public float smoothTime = 0.1f;
    public Transform cameraTransform;

    [SerializeField]
    private PlayerInput playerControls;
    private InputAction move;
    private InputAction look;

    [SerializeField]
    private Rigidbody rb;

    private Vector2 moveDirection;
    private Vector2 lookInput;  
    private Vector2 currentLookRotation;
    private Vector2 lookRotationVelocity;
    private float verticalLookRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        move = playerControls.actions.FindAction("Move");
        look = playerControls.actions.FindAction("Look");
    }

    private void Update()
    {
        ReadMove();
        ReadLook();
        PlayerActions();
    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    private void ReadMove()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void ReadLook()
    {
        lookInput = look.ReadValue<Vector2>();
    }

    private void PlayerActions() 
    {
        //To do
    }

    private void Move()
    {
        rb.velocity = moveSpeed * (transform.forward * moveDirection.y + transform.right * moveDirection.x);
    }

    private void Look()
    {
        // Apply smoothing to the mouse input
        currentLookRotation.x = Mathf.SmoothDamp(currentLookRotation.x, lookInput.x, ref lookRotationVelocity.x, smoothTime);
        currentLookRotation.y = Mathf.SmoothDamp(currentLookRotation.y, lookInput.y, ref lookRotationVelocity.y, smoothTime);

        // Horizontal rotation (left/right)
        transform.Rotate(Vector3.up * currentLookRotation.x * lookSpeed * Time.deltaTime);

        // Vertical rotation (up/down)
        verticalLookRotation -= currentLookRotation.y * lookSpeed * Time.deltaTime;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }
}
