using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public Animator leverAnim;
    public GameObject movingPlatform;
    private HoriMoving movingPlat;
    private float animationTime = 0f;
    private float animationDelay = 1.5f;
    public bool canMove = true;
    bool moveTOA = true;
    bool playAnim = false;
    // Start is called before the first frame update
    void Start()
    {
        movingPlat = movingPlatform.GetComponent<HoriMoving>();
    }
     
    // Update is called once per frame
    void Update()
    {
        if(movingPlatform.transform.position == movingPlat.pointA.position) {
            moveTOA = false;
        }
        if (movingPlatform.transform.position == movingPlat.pointB.position) {
            moveTOA = true;
        }
        if (Input.GetKeyDown(KeyCode.F) && canMove) {    
            canMove = false;
            PlayAnimation();
            if (moveTOA) {
                movingPlat.movingTarget = 1;
            }
            else if (!moveTOA) {
                movingPlat.movingTarget = 0;
            }
        }
        if (playAnim) {
            animationTime += 1 * Time.deltaTime;
            if (animationTime >= animationDelay) {
                leverAnim.SetBool("triggerLever", false);
                animationTime = 0f;
                playAnim = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canMove = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canMove = false;
        }
    }

    private void PlayAnimation() {
        playAnim = true;
        leverAnim.SetBool("triggerLever", true);
    }


}
