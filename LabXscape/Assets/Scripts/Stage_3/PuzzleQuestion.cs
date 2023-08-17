using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleQuestion : MonoBehaviour
{
    public GameObject puzzleTextQuestion;
    public GameObject puzzleTextSuccess;

    public Stage3Puzzle stage3PuzzleManager;

    public bool playerIsNear = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsNear && Input.GetKeyDown(KeyCode.F)){
            puzzleTextQuestion.SetActive(true);
        }
        else if(playerIsNear && Input.GetKeyDown(KeyCode.F) && stage3PuzzleManager.success){
            puzzleTextSuccess.SetActive(true);
        }
        else if(!playerIsNear){
            puzzleTextSuccess.SetActive(false);
            puzzleTextQuestion.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerIsNear = false;
        }
    }
}
