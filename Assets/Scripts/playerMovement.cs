using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public bool isGrounded = true;
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

        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Keyboard.current.wKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            transform.localScale = new Vector3(
                normalScale.x,
                normalScale.y * 0.5f,
                normalScale.z
            );
        }
        else
        {
            transform.localScale = normalScale;
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
}
