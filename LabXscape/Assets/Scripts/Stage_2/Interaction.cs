using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public PuzzleManager puzzle;
    public bool playerIsOnPC1 = false, playerIsOnPC2 = false, playerIsOnPC3 = false;
    
    // Start is called before the first frame update
    void Start()
    {
        puzzle.interactionFirst = false;
        puzzle.interactionSecond = false;
        puzzle.interactionThird = false;

        puzzle.lightA_Active = false;
        puzzle.lightB_Active = false;
        puzzle.lightC_Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsOnPC1 && Input.GetKeyDown(KeyCode.F))
        {
            puzzle.interactionFirst = true;
            puzzle.lightA_Active = true;
        }

        else if (playerIsOnPC2 && Input.GetKeyDown(KeyCode.F) && puzzle.interactionFirst)
        {
            puzzle.interactionSecond = true;
            puzzle.lightB_Active = true;
        }

        else if (playerIsOnPC3 && Input.GetKeyDown(KeyCode.F) && puzzle.interactionFirst && puzzle.interactionSecond)
        {
            puzzle.interactionThird = true;
            puzzle.lightC_Active = true;
        }
        else
        {
            puzzle.interactionFirst = false;
            puzzle.lightA_Active = false;
        }

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stage2PC1"))
        {
            playerIsOnPC1 = true;
        }
        if (collision.gameObject.CompareTag("Stage2PC2"))
        {
            playerIsOnPC2 = true;
        }
        if (collision.gameObject.CompareTag("Stage2PC3"))
        {
            playerIsOnPC3 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Stage2PC1"))
        {
            playerIsOnPC1 = false;
        }
        if (collision.gameObject.CompareTag("Stage2PC2"))
        {
            playerIsOnPC2 = false;
        }
        if (collision.gameObject.CompareTag("Stage2PC3"))
        {
            playerIsOnPC3 = false;
        }
    }
}
