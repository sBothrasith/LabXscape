using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockedDoor : MonoBehaviour
{

    public CharacterObject playerObject;

    private ElevatorTrigger elevatorTriggerScript;
    private LevelMoveScript nextLevelScript;
    private Animator doorAnimator;
    private WordPuzzleSolveManager wordPuzzleSolveManager;

    public GameObject requiredText;
    // Start is called before the first frame update
    void Start()
    {
        elevatorTriggerScript =  GetComponent<ElevatorTrigger>();
        nextLevelScript = GetComponent<LevelMoveScript>();
        doorAnimator = GetComponent<Animator>();
        wordPuzzleSolveManager = FindObjectOfType<WordPuzzleSolveManager>();
        requiredText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DoorUnlock();
    }

    void DoorUnlock() {

        if(playerObject.collectedObject.Contains("Card")) {
            elevatorTriggerScript.enabled = true;
            if (wordPuzzleSolveManager != null && wordPuzzleSolveManager.solved == true) {
                nextLevelScript.enabled = true;
            }
            doorAnimator.enabled = true;
        }
        else {
            elevatorTriggerScript.enabled = false;
            nextLevelScript.enabled = false;
            doorAnimator.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            if (!playerObject.collectedObject.Contains("Card")){
                requiredText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (!playerObject.collectedObject.Contains("Card")) {
                requiredText.SetActive(false);
            }
        }
    }
}
