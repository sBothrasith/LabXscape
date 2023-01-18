using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }
    public float runMaxSpeed = 9.5f;
    public float runAccelRate = 9.5f;
    public float jumpForce = 10.0f;

    public Transform groundCheck;
    [SerializeField ] public LayerMask ground;
    private bool isOnGround;
    private Vector2 moveInput;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Update() {
        CheckGround();
        PlayerMovement();
        PlayerJump();

        
    }


    private void PlayerMovement() {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
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
    }

    private void CheckGround() {
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.3f, ground);
    }
}
