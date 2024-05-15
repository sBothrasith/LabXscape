using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class WordPuzzleSolveManager : MonoBehaviour
{
    private LevelMoveScript nextStageDoor;
    public bool solved = false;

    private int solvedWord = 0;
    private List<string> currentWord;
    private int currentIndex;

    private UILetterController letterController;
    private WordPuzzleManager wordPuzzleManager;

    private List<(string letter, bool collected)> characterToCollectList = new();

    private void Awake() {
        wordPuzzleManager = gameObject.GetComponent<WordPuzzleManager>();
        letterController = FindObjectOfType<UILetterController>();
        nextStageDoor = FindObjectOfType<LevelMoveScript>();
    }

    private void Start() {
        currentIndex = 0;
        currentWord = wordPuzzleManager.GetLetterListToSolve().ElementAt(currentIndex).Value;

        StringToList();
    }

    private void StringToList() {
        if (currentWord == null) {
            return;
        }

        foreach (string s in currentWord) {
            characterToCollectList.Add((s, false));
        }
    }

    public bool CollectLetter(string l) {
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

    public List<(string letter, bool collected)> GetCharacterToCollectList() {
        return characterToCollectList;
    }

    public void SolvedWord () {
        solvedWord++;
        if (solvedWord < wordPuzzleManager.numberOfWordToSolve) {
            characterToCollectList.Clear();
            currentIndex++;
            currentWord = wordPuzzleManager.GetLetterListToSolve().ElementAt(currentIndex).Value;
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
            StartCoroutine(WaitForNewWord());
        }
    }

    IEnumerator WaitForNewWord() {
        StartCoroutine(PlaySuccessParticle());
        yield return new WaitForSeconds(2.5f);
        SolvedWord();
    }

    IEnumerator PlaySuccessParticle() {
        ParticleSystem winParticle = GameObject.Find("Win Particle").GetComponent< ParticleSystem>();
        winParticle.Play();
        yield return new WaitForSeconds(1.5f);
        winParticle.Stop();
    }
}
