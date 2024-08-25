using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 100f;
    public Transform cameraTransform;

    [SerializeField]
    private PlayerInput playerControls;
    private InputAction move;
    private InputAction look;

    [SerializeField]
    private Rigidbody rb;

    private Vector2 moveDirection;
    private Vector2 lookInput;
    private float verticalLookRotation = 0f;

    private void Start()
    {
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
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.y * moveSpeed);
    }

    private void Look()
    {
        // Horizontal rotation (left/right)
        transform.Rotate(Vector3.up * lookInput.x * lookSpeed * Time.deltaTime);

        // Vertical rotation (up/down)
        verticalLookRotation -= lookInput.y * lookSpeed * Time.deltaTime;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f); // Limit the vertical look rotation

        cameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }
}
