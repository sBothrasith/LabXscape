using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WordPuzzleSolveManager : MonoBehaviour
{
    private LevelMoveScript nextStageDoor;
    public bool solved = false;

    private int solvedWord = 0;
    private string currentWord;
    private int currentIndex;

    private UILetterController letterController;
    private WordPuzzleManager wordPuzzleManager;

    private List<(char letter, bool collected)> characterToCollectList = new();

    private void Awake() {
        wordPuzzleManager = gameObject.GetComponent<WordPuzzleManager>();
        letterController = FindObjectOfType<UILetterController>();
        nextStageDoor = FindObjectOfType<LevelMoveScript>();
    }

    private void Start() {
        currentIndex = 0;
        currentWord = (string)wordPuzzleManager.getWordListToSolve()[currentIndex];

        StringToList();
    }

    private void StringToList() {
        if (currentWord == null) {
            return;
        }

        foreach (char c in currentWord) {
            characterToCollectList.Add((c, false));
        }
    }

    public bool CollectLetter(char l) {
        for (int i = 0; i < characterToCollectList.Count; i++) {
            if (characterToCollectList[i].letter == l && !characterToCollectList[i].collected) {
                characterToCollectList[i] = (characterToCollectList[i].letter, true);
                letterController.UpdateUILetter();
                CheckSolveLetter();
                return true;
            }
        }

        return false;
    }

    public List<(char letter, bool collected)> GetCharacterToCollectList() {
        return characterToCollectList;
    }

    public void SolvedWord () {
        solvedWord++;
        if (solvedWord < wordPuzzleManager.numberOfWordToSolve) {
            characterToCollectList.Clear();
            currentIndex++;
            currentWord = (string)wordPuzzleManager.getWordListToSolve()[currentIndex];
            StringToList();
            letterController.UpdateUIContainer();
        } else {
            nextStageDoor.GetComponent<LevelMoveScript>().enabled = true;
            solved = true;
            letterController.ClearContainer();
        }
    }

    public void CheckSolveLetter() {
        bool allCharacterCollected = characterToCollectList.All(item => item.collected);

        if (allCharacterCollected) {
            SolvedWord();
        }
    }
}