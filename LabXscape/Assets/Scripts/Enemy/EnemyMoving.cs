using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentPointIndex = 0;
    [SerializeField] private float speed = 2f;

	public GameObject spawnpoint;
	public PlayerControllerMovement player;
	private float countdown = 0f;
	private float deathCountDown = 2f;
	private bool playDSound = false;


	// Update is called once per frame
	void Update()
    {
        if (Vector2.Distance(waypoints[currentPointIndex].transform.position, transform.position) < .1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= waypoints.Length)
            {
                transform.localScale = new Vector3(-1f, transform.localScale.y,transform.localScale.z);
                currentPointIndex = 0;
            }
            else
            {
                transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentPointIndex].transform.position, Time.deltaTime * speed);

		if (PlayerDie())
		{
			countdown += 1 * Time.deltaTime;
			Die();
			if (countdown >= deathCountDown)
			{
				player.gameObject.transform.position = spawnpoint.transform.position;
				player.enabled = true;
				player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
				countdown = 0f;
				playDSound = false;
			}
		}

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.position.x > transform.position.x)
        {
            collision.transform.SetParent(transform);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDie();
			if (!playDSound)
			{
				playDSound = true;
				FindObjectOfType<AudioManager>().Play("Death");
			}
		}
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);

    }

    public bool PlayerDie()
    {
        return true;
    }

	private void Die()
	{
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		player.enabled = false;
	}
}
