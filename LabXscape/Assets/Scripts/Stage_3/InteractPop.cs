using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPop : MonoBehaviour
{
	bool popTrigger = false;

	public TextMeshPro nextText;

	// Start is called before the first frame update
	void Start()
	{
		nextText.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			popTrigger = true;
			nextText.enabled = true;
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			popTrigger = false;
			nextText.enabled = false;
		}
	}
}
