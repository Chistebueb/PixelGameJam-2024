using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 7.0f;

    [Header("Camera Settings")]
    public GameObject headObject;
    public GameObject cameraObject;
    public static float lookSensitivity = 2.0f;
    public float lookUpLimit = 50.0f;
    public float lookDownLimit = -50.0f;

    [Header("UI Settings")]
    public Text speedText;

    [Header("Key Bindings")]
    public static KeyCode jumpKey = KeyCode.Space;
    public static KeyCode sprintKey = KeyCode.LeftShift;
    public static KeyCode crouchKey = KeyCode.C;

    [Header("Goon")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] float maxDistance = 100f;

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

        if (Input.GetMouseButtonDown(0)) 
        {

            if (gunAnimator != null)
            {
                gunAnimator.StopPlayback(); 
                gunAnimator.Play("shot");   
            }
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, moveDirection, out hit, 1f))
        {
            float currentSpeed = moveSpeed;
            if (Input.GetKey(sprintKey))
            {
                currentSpeed = sprintSpeed;
            }
            else if (Input.GetKey(crouchKey))
            {
                currentSpeed = crouchSpeed;
            }

            Vector3 horizontalVelocity = moveDirection.normalized * currentSpeed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
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

        headObject.transform.localEulerAngles = new Vector3(rotationX, 0f, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleCrouch()
    {
        if (Input.GetKey(crouchKey))
        {
            headObject.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
        else
        {
            headObject.transform.localPosition = new Vector3(0, 1f, 0);
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

public void Shoot()
    {
        Camera camera = cameraObject.GetComponent<Camera>();

        Vector3 point = new Vector3(0.5f, 0.5f, 0);

        Ray ray = camera.ViewportPointToRay(point);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyHit>().Die();
            }
        }
    }


}
