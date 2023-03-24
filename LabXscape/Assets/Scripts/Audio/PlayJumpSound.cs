using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayJumpSound : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            FindObjectOfType<AudioManager>().Play("EndJump");
        }
    }
}
