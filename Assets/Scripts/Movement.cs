using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    
    public float speed = 5f;


    public float jumpForce = 7f;
    public bool isGrounded = true;

    [Header("Controls")]
    private Vector3 moveInput;
    private bool isDashing = false;
    private bool canDash = true;
    private float dashEndTime = 0f;
    public float dashForce = 10f;
    public float dashCoolDown = 2.5f;
    public float dashDuration = 0.2f;   
    public float dashingTime = 1f;


    private Rigidbody rb;
    private Vector3 normalScale;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        normalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        
        if (Keyboard.current.dKey.isPressed)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if ((Keyboard.current.wKey.wasPressedThisFrame) && (isGrounded))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }



        if (Keyboard.current.spaceKey.wasPressedThisFrame && canDash)
        {
            StartCoroutine(Dash());
        }

        if (Keyboard.current.sKey.isPressed)
        {
            transform.localScale = new Vector3(
                normalScale.x,
                normalScale.y * 0.5f,
                normalScale.z
            );
        }else
        {
            transform.localScale = normalScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector3 dashDirection = moveInput;
        rb.linearVelocity = dashDirection * dashForce;
        // Trzeba orientacje ogarn¹æ  z move input najlepiej nowa metoda 
        dashEndTime = Time.time + dashCoolDown;

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
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
