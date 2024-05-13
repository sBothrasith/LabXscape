using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMovement : MonoBehaviour, IDataPersistence
{
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private Vector2 newVelocity;
    private Vector2 colliderSize;
    private Vector2 slopeNormalPerp;
    private Vector2 newForce;

    private float moveInput;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;

    private bool isJumping;
    private bool canJump;


	[Range(0.0f, 50.0f)] public float moveSpeed = 8f;
    [Range(0.0f, 50.0f)] public float jumpHeight = 14f;
    [SerializeField] private float slopeCheckDistance;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public float groundCheckRadius;
    public bool doubleJumped;
    public bool isOnSlope;
    public bool isWalking = false;
    public Dialogue dialogue;

    public Animator animator;

    public AudioSource runningSound;

    [Header("Particle")]
    [SerializeField] private ParticleSystem movementParticle;

    [Range(0, 0.2f)]
    [SerializeField] private float dustFormationPeriod;

    private float particleCounter;

    LightUp lightDie;
    EnemyMoving enemyDie;

    private void Start()
    {
        runningSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        colliderSize = cc.size;
        animator = GetComponent<Animator>();
        SetGravityScale(1.0f);
    }

    private void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        HandleLayers();
    }

    private void Update()
    {
        PlayerMovement();
        PlayerJump();  
    }


    private void PlayerMovement()
    {
        if(dialogue != null)
        {
			if (dialogue.dialogueActive)
			{
				rb.velocity = Vector2.zero;
				return;
			}
		}   
        
        moveInput = Input.GetAxisRaw("Horizontal");
        newVelocity.Set(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = newVelocity;

        if(isGrounded && !isOnSlope && !isJumping)
        {
			newVelocity.Set(moveInput * moveSpeed, 0.0f);
			rb.velocity = newVelocity;
		}
        else if (isGrounded && isOnSlope && !isJumping)
        {
			newVelocity.Set(moveSpeed * slopeNormalPerp.x * -moveInput, moveSpeed * slopeNormalPerp.y * -moveInput);
			rb.velocity = newVelocity;
		}
        else if (!isGrounded)
        {
			newVelocity.Set(moveInput * moveSpeed, rb.velocity.y);
			rb.velocity = newVelocity;
		}

        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        if (moveInput == 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
        }
        if (moveInput < 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
        else if (moveInput > 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }


        if (Input.GetButton("Horizontal") && isGrounded == true)
        {
            runningSound.enabled = true;
            isWalking = true;
            PlayMovementParticle();
        }
        else
        {
            runningSound.enabled = false;
            isWalking = false;
        }
    }

    private void CheckGround()
    {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		if (rb.velocity.y <= 0.0f)
		{
			isJumping = false;
		}

		if (isGrounded && !isJumping)
		{
			canJump = true;
		}
	}

    private void SlopeCheck()
    {
        Vector2 checkPosSlope = transform.position - new Vector3(0.0f, colliderSize.y / 2);

        SlopeCheckHorizontal(checkPosSlope);
        SlopeCheckVertical(checkPosSlope);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

	private void SlopeCheckVertical(Vector2 checkPos)
	{
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawLine(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawLine(hit.point, hit.normal, Color.green);
            
        }
	}

	private void PlayerJump()
    {
        
        if (isGrounded || isOnSlope)
        {
            animator.ResetTrigger("IsJumping");
            animator.SetBool("IsFalling", false);
            doubleJumped = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("IsJumping");
            Jump();
        }
        if (Input.GetButtonDown("Jump") && !isGrounded && !doubleJumped)
        {
            doubleJumped = true;
            animator.SetTrigger("IsJumping");
            DoubleJump();

        }

        if (Input.GetButtonDown("Jump"))
        {
            SetGravityScale(2.0f);
        }
        else
        {
            SetGravityScale(5.0f);
        }

        if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("IsFalling", true);
            animator.ResetTrigger("IsJumping");
        }

        if(rb.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
        }

    }

    public void Jump()
    {
        if(canJump)
        {
            isJumping = true;
			PlayJumpSound.canPlayFall = true;
			FindObjectOfType<AudioManager>().Play("StartJump");
			rb.freezeRotation = true;
			newVelocity.Set(0.0f, 0.0f);
			rb.velocity = newVelocity;
			newForce.Set(0.0f, jumpHeight);
			rb.AddForce(newForce, ForceMode2D.Impulse);
		}
	}
    public void DoubleJump()
    {
        FindObjectOfType<AudioManager>().Play("StartJump");
        rb.freezeRotation = true;
		newVelocity.Set(0.0f, 0.0f);
		rb.velocity = newVelocity;
		newForce.Set(0.0f, jumpHeight);
		rb.AddForce(newForce, ForceMode2D.Impulse);
	}

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }
    private void SetGravityScale(float gravityScale)
    {
        rb.gravityScale = gravityScale;
    }

    private void PlayMovementParticle()
    {
        particleCounter += Time.deltaTime;

        if (particleCounter > dustFormationPeriod)
        {
            movementParticle.Play();
            particleCounter = 0;
        }
    }

    public void LoadGameData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveGameData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }
}