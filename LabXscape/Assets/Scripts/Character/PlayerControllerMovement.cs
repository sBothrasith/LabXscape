using Spine.Unity;
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
    public bool isWalking = false;

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, running, jumping, death;
    public string currentState;
    public string previousState;
    public string currentAnimation = "";

    public AudioSource runningSound;

    private void Start()
    {
        runningSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        currentState = "idle";
        SetCharacterState(currentState);
        SetGravityScale(1.0f);
    }
    
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Update()
    {
        PlayerMovement();
        PlayerJump();
    }

    public void SetCharacterAnimation(AnimationReferenceAsset animation, bool loop, float timeScale) {
        if(animation != null) {
            if (animation.name.Equals(currentAnimation)) {
                return;
            }
            Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
            animationEntry.TimeScale = timeScale;
            animationEntry.Complete += AnimationEntry_Complete;
            currentAnimation = animation.name;
        }
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry) {
        if(currentState.Equals("jumping")) {
            SetCharacterState(previousState);
        }
    }

    public void SetCharacterState(string state) {
        if (state.Equals("idle")) {
            SetCharacterAnimation(idle, true, 1f);
        }else if (state.Equals("running")) {
            SetCharacterAnimation(running, true, 2f);
        }else if (state.Equals("jumping")) {
            SetCharacterAnimation(jumping, false, 1f);
        }else if (state.Equals("death")) {
            SetCharacterAnimation(death, false, 1f);    
        }
        currentState= state;
    }


    private void PlayerMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        if(moveInput.x == 0) {
            if (!currentState.Equals("jumping")) {
                SetCharacterState("idle");
            }
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
        }
        if (moveInput.x < 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (!currentState.Equals("jumping")) {
                SetCharacterState("running");
            }
        }  
        else if (moveInput.x > 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (!currentState.Equals("jumping")) {
                SetCharacterState("running");
            }
        }

        if (inSlope)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
        }
        else
        {
            if (!inSlope && isWalking)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.freezeRotation = true;
            }
        }

        if (Input.GetButton("Horizontal") && inSlope || Input.GetAxis("Horizontal") > 0.5f && inSlope || Input.GetAxis("Horizontal") < -0.5f && inSlope)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            inSlope = false;
            isWalking = true;
        }

        if (Input.GetButton("Horizontal") && isGrounded == true)
        {
            runningSound.enabled = true;
            isWalking = true;
        }
        else
        {
            runningSound.enabled = false;
            isWalking = false;
        }
    }

    private void PlayerJump()
    {
        if (isGrounded)
        {
            doubleJumped = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            if (!currentState.Equals("jumping")) {
                previousState = currentState;
            }
            SetCharacterState("jumping");
        }
        if (Input.GetButtonDown("Jump") && !isGrounded && !doubleJumped)
        {
            DoubleJump();
            doubleJumped = true;
            if (!currentState.Equals("jumping")) {
                previousState = currentState;
            }
            SetCharacterState("jumping");
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
        FindObjectOfType<AudioManager>().Play("StartJump");
        rb.freezeRotation = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight); 
    }
    public void DoubleJump()
    {
        FindObjectOfType<AudioManager>().Play("StartJump");
        rb.freezeRotation = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
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
