using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserLever : MonoBehaviour
{
    public Animator leverAnim;
    public GameObject triggerLaser;
    public Light2D leverLight;
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
            if (on) {
                triggerLaser.SetActive(false);
                on = false;
				leverLight.color = Color.red;

			}
			else {
                triggerLaser.SetActive(true);
                on = true;
				leverLight.color = Color.green;

			}
			PlayAnimation(on);
		}
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canMove = true;
        }
    }
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
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
