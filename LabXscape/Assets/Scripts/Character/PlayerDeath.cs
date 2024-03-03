using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;

    public PlayerControllerMovement player;
    public GameObject spawnpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        player = player.GetComponent<PlayerControllerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CrushZone"))
        {
            Die();
        }
    }

    private void Die()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.enabled = false;
    }
}
