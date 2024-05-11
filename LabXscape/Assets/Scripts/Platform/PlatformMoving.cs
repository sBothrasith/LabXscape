using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentPointIndex = 0;

    [SerializeField] private float speed = 1f;

    private Collider2D _collider;
    private bool _playerOnPlatform;


	private void Start()
	{
		_collider = GetComponent<Collider2D>();
	}
	void FixedUpdate()
    {

        if (Vector2.Distance(waypoints[currentPointIndex].transform.position, transform.position) < .1f){
            currentPointIndex++;
            if (currentPointIndex >= waypoints.Length)
            {
                currentPointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentPointIndex].transform.position, Time.deltaTime * speed);
    }

	private void Update()
	{
		if(_playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            _collider.enabled = false;
            StartCoroutine(EnabledCollider());
        }
	}

    IEnumerator EnabledCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

	private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<PlayerControllerMovement>();
        if (player != null)
        {
            _playerOnPlatform = value;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
        SetPlayerOnPlatform(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
		SetPlayerOnPlatform(collision, true);
	}

}
