using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Range(0.0f, 50.0f)] public float moveSpeed = 8f;
    [Range(0.0f, 50.0f)] public float jumpHeight = 14f;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public float groundCheckRadius;
    public bool doubleJumped;
    public bool inSlope = false;
    public bool walking = false;

    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetGravityScale(1.0f);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    }
    // Update is called once per frame
    private void Update()
    {
        PlayerMovement();
        PlayerJump();
        
    }

    private void PlayerMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        if (inSlope)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            if (!inSlope && walking)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.freezeRotation = true;
            }
        }
        // To can walk thru the slopes we return to activate the horizontal movement but only when we move the joystick or the arrow keys.
        if (Input.GetButton("Horizontal") && inSlope || Input.GetAxis("Horizontal") > 0.5f && inSlope || Input.GetAxis("Horizontal") < -0.5f && inSlope)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            inSlope = false;
            walking = true;

        }

        if (Input.GetButton("Horizontal") && isGrounded == true)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            doubleJumped = false;
        }
        if (Input.GetButtonDown("Jump") && !isGrounded && !doubleJumped)
        {
            DoubleJump();
            doubleJumped = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            SetGravityScale(2.0f);
        }
        else
        {
            SetGravityScale(5.0f);
        }

    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight); // We determine the new position of the player based on the Rigidbody's x velocity and the jump amount.
    }
    public void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);// We determine the new position of the player based on the Rigidbody's x velocity and the jump amount.

    }

    private void SetGravityScale(float gravityScale)
    {
        rb.gravityScale = gravityScale;
    }

	void OnCollisionEnter2D(Collision2D coll)
	{
        if (coll.transform.CompareTag(("Slope")))
        {
            inSlope = true;
        }
    }

	void OnCollisionExit2D(Collision2D coll)
	{
        if (coll.transform.CompareTag(("Slope")))
        {
            inSlope = false;
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.transform.CompareTag(("Slope")))
		{
			inSlope = true;
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.CompareTag(("Slope")))
		{
			inSlope = false;
			rb.constraints = RigidbodyConstraints2D.None;
		}

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.transform.CompareTag(("Slope")))
		{
			inSlope = true;
		}
		
	}
}
