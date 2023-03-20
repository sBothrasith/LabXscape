using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public Animator leverAnim;

    public GameObject movingPlatform;
    public Transform pointA;
    public Transform pointB;
    private PlatformMoving movingPlat;

    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        movingPlat = movingPlatform.GetComponent<PlatformMoving>();
        movingPlat.enabled = false;
    }
     
    // Update is called once per frame
    void Update()
    {
        if(movingPlatform.transform.position.x >= pointA.position.x -0.1f) {
            leverAnim.SetBool("triggerLever", false);
            movingPlat.enabled = false;
            canMove = true;
        }
        if (movingPlatform.transform.position.x <= pointB.position.x + 0.1f) {
            leverAnim.SetBool("triggerLever", false);
            movingPlat.enabled = false;
            canMove = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.F) && canMove) {
                leverAnim.SetBool("triggerLever", true);
                movingPlat.enabled = true;
                canMove = false;
            }
        }
    }
}
