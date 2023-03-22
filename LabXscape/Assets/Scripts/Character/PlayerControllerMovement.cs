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
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
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

	// This collision use is to ensure that when the character is on a platform we stay on it.
	void OnCollisionExit2D(Collision2D other)
	{
		if (other.transform.CompareTag("MovingPlatform"))
		{
			rb.interpolation = RigidbodyInterpolation2D.Interpolate; // We activate the interpolate function to avoid the player vibration.

			transform.parent = null; // When the player leaves the platform this function is automatically disabled.
		}

	}

	// if the player collides with a gem we automatically activate the bool function of each gem that we touch.
	void OnTriggerEnter2D(Collider2D other)
	{
		// INS SLOPE FREEZZE//
		// this collision warns us that we are in an slope in order to freeze the character motion and not begin to slide down the slope.
		if (other.transform.CompareTag(("Slope")))
		{
			inSlope = true;
		}

		// MOVING PLATFORM PARENTING // 
		// This collision warns us that we are in a moving platform and parent the character and the platform to ensure that the character follows platform movement.
		if (other.transform.CompareTag("MovingPlatform"))
		{
			rb.interpolation = RigidbodyInterpolation2D.None;
			transform.parent = other.transform;
		}
	}

	//We use the "onTriggerExit2D" function to indicate the character that has stopped touching the ground, a wall .... And so to stop emitting particles or to call other functions.
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.CompareTag(("Slope")))
		{
			inSlope = false;
			rb.constraints = RigidbodyConstraints2D.None;
		}

		if (other.transform.CompareTag("MovingPlatform"))
		{
			rb.interpolation = RigidbodyInterpolation2D.Interpolate;
			transform.parent = null;
		}

	}
	//We use the "onTriggerStay2D" function to indicate the character that it's still touching the ground, a wall ....

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.transform.CompareTag(("Slope")))
		{
			inSlope = true;
		}
		//This collision warns us that we are in a moving platform and parent the character and the platform to ensure that the character follows platform movement.
		if (other.transform.CompareTag("MovingPlatform"))
		{
			GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
			transform.parent = other.transform;
		}

	}
}
