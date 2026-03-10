using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playerMovement : MonoBehaviour
{
    
    public float speed = 5f;


    public float jumpForce = 7f;
    public bool isGrounded = true;

    [Header("Controls")]
    private Vector3 moveInput;
    private bool isDashing = false;
    private bool canDash = true;
    public float dashForce = 10f;
    public float dashCoolDown = 2.5f;
    public float dashDuration = 0.2f;   
    public float dashingTime = 1f;

    [Header("Crouch")]
    public float crouchSpeed = 2.5f;
    public float crouchHeight = 0.5f;
    public LayerMask ceilingLayer;

    private bool isCrouching = false;
    private Vector3 standingScale;



    private Rigidbody rb;
    private Vector3 normalScale;
    private float horizontal;

    [Header("UI")]
    public Image dashCooldownImage;
    private float dashCooldownTimer = 0f;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        normalScale = transform.localScale;
        standingScale = transform.localScale;
    }

    // Update is called once per frame
    

    void Update()
    {
        horizontal = 0f;

        if (Keyboard.current.aKey.isPressed)
            horizontal = -1f;
            

        if (Keyboard.current.dKey.isPressed)
            horizontal = 1f;

        if (Keyboard.current.wKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }


        if (Keyboard.current.sKey.isPressed)
        {
            Crouch();
        }
        else
        {
            TryStandUp();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && canDash && !isCrouching)
        {
            StartCoroutine(Dash());
        }


        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            dashCooldownImage.fillAmount = dashCooldownTimer / dashCoolDown;
        }
        else
        {
            dashCooldownImage.fillAmount = 0f;
        }

        Rotate();
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        float currentSpeed = isCrouching ? crouchSpeed : speed;
        rb.linearVelocity = new Vector3(horizontal * currentSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

    }

    void Rotate()
    {
        float yRotation = transform.eulerAngles.y;


        if ((Mathf.Approximately(yRotation, 0f)) && (Keyboard.current.dKey.wasPressedThisFrame))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if ((Mathf.Approximately(yRotation, 180f)) && (Keyboard.current.aKey.wasPressedThisFrame))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }


    private IEnumerator Dash()
    {
        canDash = false;
        dashCooldownTimer = dashCoolDown;


        Vector3 dashDirection = new Vector3(horizontal, 0, 0);

        if (dashDirection == Vector3.zero)
            dashDirection = transform.right;

        dashDirection.Normalize();

        if (Physics.Raycast(transform.position, dashDirection, 0.7f))
        {
            canDash = true;
            yield break;
        }

        isDashing = true;

        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, rb.linearVelocity.z);
        rb.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, rb.linearVelocity.z);
        isDashing = false;

        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
    void Crouch()
    {
        if (isCrouching) return;

        isCrouching = true;
        transform.localScale = new Vector3(
            standingScale.x,
            crouchHeight,
            standingScale.z
        );
    }

    void TryStandUp()
    {
        if (!isCrouching) return;

        if (Physics.Raycast(transform.position, Vector3.up, 1f, ceilingLayer))
            return;

        isCrouching = false;
        transform.localScale = standingScale;
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("NextLVL"))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            int SceneID = activeScene.buildIndex;
            SceneManager.LoadScene(SceneID+1);
        }

        if (collision.gameObject.CompareTag("PrevLVL"))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            int SceneID = activeScene.buildIndex;
            SceneManager.LoadScene(SceneID + 1);
        }
    }
}
