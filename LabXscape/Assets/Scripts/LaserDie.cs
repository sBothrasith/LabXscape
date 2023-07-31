using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDie : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnPoint;
    public PlayerControllerMovement player;
    public float laserLength;

    private bool die = false;
    private float countdown = 0f;
    private float deathCountDown = 2f;
    private bool playDSound = false;
    void Start()
    {
        player = player.GetComponent<PlayerControllerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLaser();
        Die();
    }

    void CheckLaser() {
        if(Vector3.Distance(player.transform.position, this.GetComponent<LineRenderer>().GetPosition(1))< laserLength) {
            die = true;
            if (!playDSound) {
                playDSound = true;
                FindObjectOfType<AudioManager>().Play("Death");
            }
        }
        
    }

    void Die() {
        if (die) {
            countdown += 1 * Time.deltaTime;
            player.SetCharacterState("death");
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            player.enabled = false;
            if (countdown >= deathCountDown) {
                player.gameObject.transform.position = spawnPoint.transform.position;
                player.enabled = true;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                player.SetCharacterState("idle");
                countdown = 0f;
                playDSound = false;
                die = false;
            }
        }
    }

}
