using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Serialized fields for customization in the Unity Editor
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 7.0f;

    [Header("Camera Settings")]
    public GameObject cameraObject;
    public float lookSensitivity = 2.0f;
    public float lookUpLimit = 50.0f;
    public float lookDownLimit = -50.0f;

    [Header("UI Settings")]
    public Text speedText;

    [Header("Key Bindings")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Goon")]
    [SerializeField] private Animator gunAnimator;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float rotationX = 0;

    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCameraRotation();
        HandleCrouch();
        UpdateSpeedUI();

        if (Input.GetMouseButtonDown(0))  // Check for left mouse button click
        {
            Shoot();  // Execute the shoot method

            if (gunAnimator != null)
            {
                gunAnimator.StopPlayback();  // Stop any current animation
                gunAnimator.Play("shot");    // Play the "shot" animation
            }
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float currentSpeed = moveSpeed;
        if (Input.GetKey(sprintKey))
        {
            currentSpeed = sprintSpeed;
        }
        else if (Input.GetKey(crouchKey))
        {
            currentSpeed = crouchSpeed;
        }

        moveDirection = transform.forward * vertical + transform.right * horizontal;
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleCameraRotation()
    {
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, lookDownLimit, lookUpLimit);

        cameraObject.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleCrouch()
    {
        if (Input.GetKey(crouchKey))
        {
            cameraObject.transform.localPosition = new Vector3(0, -0.5f, 0); // Adjust this value as needed
        }
        else
        {
            cameraObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    private void UpdateSpeedUI()
    {
        if (speedText != null)
        {
            float speed = rb.velocity.magnitude;
            float horizontalSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
            speedText.text = $"Speed: {horizontalSpeed:F2}";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    private void Shoot()
    {
         
    }

}
