using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public Animator leverAnim;
    public GameObject movingPlatform;
    private HoriMoving movingPlat;

    public bool canMove = true;
    bool moveTOA = true;
    // Start is called before the first frame update
    void Start()
    {
        movingPlat = movingPlatform.GetComponent<HoriMoving>();
    }
     
    // Update is called once per frame
    void Update()
    {
        if(movingPlatform.transform.position == movingPlat.pointA.position) {
            leverAnim.SetBool("triggerLever", false);
            canMove = true;
            moveTOA = false;
        }
        if (movingPlatform.transform.position == movingPlat.pointB.position) {
            leverAnim.SetBool("triggerLever", false);
            canMove = true;
            moveTOA = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.F) && canMove) {
                leverAnim.SetBool("triggerLever", true);
                canMove = false;
                if (moveTOA) { 
                    movingPlat.movingTarget = 1;
                }
                else if (!moveTOA) {
                    movingPlat.movingTarget = 0;
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.F) && canMove) {
                leverAnim.SetBool("triggerLever", true);
                canMove = false;
                if (moveTOA) {
                    movingPlat.movingTarget = 1;
                }else if (!moveTOA) {
                    movingPlat.movingTarget = 0;
                }
            }
        }
    }
}
