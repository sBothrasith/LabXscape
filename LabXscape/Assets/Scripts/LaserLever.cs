using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLever : MonoBehaviour
{
    public Animator leverAnim;
    public GameObject triggerLaser;
    private float animationTime = 0f;
    private float animationDelay = 1.5f;
    public bool canMove = false;
    bool playAnim = false;
    bool on = true;
    // Start is called before the first frame update
    void Start() {
        leverAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && canMove) {
            canMove = false;
            PlayAnimation();
            if (on) {
                triggerLaser.SetActive(false);
                on = false;
            }
            else {
                triggerLaser.SetActive(true);
                on = true;
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
