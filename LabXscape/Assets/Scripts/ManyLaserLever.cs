using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ManyLaserLever : MonoBehaviour
{
    private Animator leverAnim;
    private Light2D leverLight;

    public GameObject[] triggerLasers;
    public bool[] activeLasers;

    public bool canMove = false;
    bool on = true;
    // Start is called before the first frame update
    void Start() {
        leverAnim = GetComponent<Animator>();
        leverLight = GetComponent<Light2D>();
        PlayAnimation(on);
        leverLight.color = Color.green;

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && canMove) {
            for (int i = 0; i < triggerLasers.Length; i++) {
                triggerLasers[i].SetActive(activeLasers[i]);
                activeLasers[i] = !activeLasers[i];
            }

            PlayAnimation(on);
            on = !on;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canMove = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canMove = false;
        }
    }

    private void PlayAnimation(bool trigger) {
        leverAnim.SetBool("triggerLever", trigger);
    }
}
