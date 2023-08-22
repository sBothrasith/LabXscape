using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{
    public GameObject requireText;

    public bool playerIsNear = false;
    public bool keyPress = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            keyPress = true;
        }
        else if(Input.GetKeyUp(KeyCode.F)){
            keyPress = false;
        }

        if (playerIsNear && keyPress)
        {
            requireText.SetActive(true);
        }
        else
        {
            requireText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
