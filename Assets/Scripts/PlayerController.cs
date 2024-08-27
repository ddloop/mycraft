using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

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

    private bool ignoreInput = false;
    public bool IgnoreInput { get { return ignoreInput; } set { rb.velocity = Vector2.zero; ignoreInput = value; } }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        move = playerControls.actions.FindAction("Move");
        look = playerControls.actions.FindAction("Look");

        playerControls.actions.FindAction("OpenUI").performed += OpenUI;
        playerControls.actions.FindAction("Interact").performed += Interact;
    }



    private void Update()
    {
        if (!ignoreInput)
        {
            ReadMove();
            ReadLook();
        }
    }

    private void FixedUpdate()
    {
        if (!ignoreInput)
        {
            Move();
            Look();
        }
    }

    private void ReadMove()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void ReadLook()
    {
        lookInput = look.ReadValue<Vector2>();
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

    private void OpenUI(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Confined;
        IgnoreInput = true;
        UIManager.Instance.OpenUI();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        RaycastHit[] hits = Physics.RaycastAll(ray, 5.0f, layerMask);

        if (hits.Length > 0)
        {
            RaycastHit closestHit = FindClosestHit(hits);
            Item item = closestHit.rigidbody.GetComponent<Item>();
            item.Enable(false);
            item.Return();
        }

        RaycastHit FindClosestHit(RaycastHit[] hits)
        {
            RaycastHit closestHit = hits[0];
            float minDistance = closestHit.distance;

            // Loop through all hits to find the closest one
            foreach (var hit in hits)
            {
                if (hit.distance < minDistance)
                {
                    closestHit = hit;
                    minDistance = hit.distance;
                }
            }

            return closestHit;
        }
    }
}
