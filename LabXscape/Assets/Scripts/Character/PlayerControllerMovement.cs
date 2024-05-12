using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMovement : MonoBehaviour, IDataPersistence
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
        animator = GetComponent<Animator>();
        SetGravityScale(1.0f);
        animator.SetBool("Die", false);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        HandleLayers();
        
    }

    private void Update()
    {
        PlayerMovement();
        PlayerJump();

        if (lightDie != null || enemyDie != null)
        {
			if (lightDie.PlayerDie() || enemyDie.PlayerDie())
			{
				animator.SetBool("Die", true);
			}
		}
        
    }


    private void PlayerMovement()
    {
        if (dialogue.dialogueActive)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        moveInput.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));

        if (moveInput.x == 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
        }
        if (moveInput.x < 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
        else if (moveInput.x > 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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
            PlayMovementParticle();
        }
        else
        {
            runningSound.enabled = false;
            isWalking = false;
        }
    }

    private void PlayerJump()
    {
        
        if (isGrounded || inSlope)
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
        PlayJumpSound.canPlayFall = true;
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag(("Slope")))
        {
            inSlope = true;
            //isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.CompareTag(("Slope")))
        {
            inSlope = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag(("Slope")))
        {
            inSlope = true;
            //isGrounded = true;
        }

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