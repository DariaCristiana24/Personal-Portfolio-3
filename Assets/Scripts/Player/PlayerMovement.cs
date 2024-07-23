using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;
    [SerializeField]
    float speed ;
    [SerializeField]
    float walkingSpeed = 10f;
    [SerializeField]
    float runningSpeed = 20f;
    [SerializeField]
    float airSpeed = 0.5f;

    [SerializeField]
    LayerMask groundMask;
    [SerializeField]
    float groundDist = 1.1f;
    [SerializeField]
    float groundDrag = 10;
    [SerializeField]
    float airDrag = 5;

    [SerializeField]
    float jumpPower = 10;

    [SerializeField]
    Transform orientation;


    void Start()
    {
        speed = walkingSpeed;
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {

            Vector3 moveVector = orientation.forward * verticalInput + orientation.right * horizontalInput;


            if (IsGrounded())
            {
                rb.AddForce(moveVector * speed * 10f, ForceMode.Force);
            }
            else
            {
                rb.AddForce(moveVector * speed * 10f * airSpeed, ForceMode.Force);
            }
    }

    private void Update()
    {
            inputs();
            groundCheck();
            if (running )
            {
                speed = runningSpeed;
            }
    }

    void inputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) )
        {
            speed = runningSpeed;
        }
        else
        {
           if (speed > walkingSpeed)
            {
                speed -= 0.1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public bool IsGrounded()
    {
        
    
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out RaycastHit hit, groundDist, groundMask))
        {
            return true;
        }

        return false;
    }

    void groundCheck()
    {
        if (IsGrounded())
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    public void Jump()
    {
        if (IsGrounded()) 
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool running = false;

}
