using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }
    public float runMaxSpeed = 9.5f;
    public float runAccelRate = 9.5f;
    public float jumpForce = 12.0f;

    public Transform groundCheck;
    [SerializeField ] public LayerMask ground;
    private bool isOnGround;

    private Vector2 moveInput;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Start() {
        SetGravityScale(1.0f);      
    }

    private void Update() {
        CheckGround();
        PlayerMovement();
        PlayerJump();

        
    }


    private void PlayerMovement() {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        CheckFacing();
        float targetSpeed = moveInput.x * runMaxSpeed;
        targetSpeed = Mathf.Lerp(rigidBody.velocity.x, targetSpeed, 1);


        float speedDiff = targetSpeed - rigidBody.velocity.x;
        float movement = speedDiff* runAccelRate;

        rigidBody.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private void PlayerJump() {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) {
            float force = jumpForce;
            if (rigidBody.velocity.y < 0) {
                force -= rigidBody.velocity.y;
            }
            rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.Space) ) {            
            SetGravityScale(2.0f);
        }
        else {
            SetGravityScale(5.0f);
        }
    }
    
    private void CheckGround() {
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.3f, ground);
        SetGravityScale(2.0f);
    }

    private void CheckFacing() {
        if(moveInput.x < 0) {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if(moveInput.x > 0) {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void SetGravityScale(float gravityScale) {
        rigidBody.gravityScale = gravityScale;
    }

}
