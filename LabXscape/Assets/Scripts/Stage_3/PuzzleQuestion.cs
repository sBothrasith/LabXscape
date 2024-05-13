    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleQuestion : MonoBehaviour
{
    public GameObject puzzleTextQuestion;

    public Stage3Puzzle stage3PuzzleManager;

    public bool playerIsNear = false;
    public bool keyPress = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        if(playerIsNear && keyPress){
            if(stage3PuzzleManager.success)
            {
                puzzleTextQuestion.SetActive(false);
            }
            else
            {
                puzzleTextQuestion.SetActive(true);
            }
        }
        else{
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
