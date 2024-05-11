using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerOnPlatform;


    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if (_playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
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
