using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPuzzleSolveManager : MonoBehaviour
{
    private List<Letter> scriptableObjectCollected = new();
    public UILetterController letterController;
    public GameObject nextStageDoor;
    private WordPuzzleManager wordPuzzleManager;

    private int solvedWord = 0;
    private string currentWord;
    private int currentIndex;

    private void Awake() {
        wordPuzzleManager = gameObject.GetComponent<WordPuzzleManager>();
    }

    private void Start() {
        currentIndex = 0;
        currentWord = (string)wordPuzzleManager.getWordListToSolve()[currentIndex];
    }

    public bool CollectLetter(Letter l) {
        if (currentWord[scriptableObjectCollected.Count] == l.GetLetter()) {
            scriptableObjectCollected.Add(l);
            letterController.UpdateUILetter();
            CheckSolveLetter();
            return true;
        } else {
            return false;
        }
    }

    private void Update() {
        
    }

    public List<Letter> GetScriptableLetter () {
        return scriptableObjectCollected;
    }

    public void SolvedWord () {
        solvedWord++;
        if (solvedWord < wordPuzzleManager.numberOfWordToSolve) {
            scriptableObjectCollected.Clear();
            letterController.wordNumber = solvedWord;
            currentIndex++;
            currentWord = (string)wordPuzzleManager.getWordListToSolve()[currentIndex];
            letterController.UpdateUIContainer();
        } else {
            nextStageDoor.GetComponent<LevelMoveScript>().enabled = true;
            letterController.ClearContainer();
        }
    }

    public bool CheckSolveLetter() {
        bool solve = false;
        for (int i = 0; i < currentWord.Length; i++) {
            if (i < scriptableObjectCollected.Count) {
                if (currentWord[i] == scriptableObjectCollected[i].GetLetter()) {
                    solve = true;
                } else {
                    solve = false;
                }
            } else if (i >= scriptableObjectCollected.Count) {
                solve = false;
            }
        }
        if (solve) {
            letterController.AddCheckBox();
            return true;
        } else {
            return false;
        }
    }
}
