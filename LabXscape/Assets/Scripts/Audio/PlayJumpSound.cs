using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayJumpSound : MonoBehaviour
{
    public ParticleSystem fallParticle;
    public static bool canPlayFall = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            if(canPlayFall) {
                AudioManager.instance.Play("EndJump");
                fallParticle.Play();
                canPlayFall = false;
            }
        }
    }
}
