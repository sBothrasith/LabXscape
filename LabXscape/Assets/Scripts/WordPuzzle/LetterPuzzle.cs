using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterPuzzle : MonoBehaviour
{
    private bool collected = false;
    private WordPuzzleSolveManager solverManager;
    private char letter;

    void Start()
    {
        solverManager = FindObjectOfType<WordPuzzleSolveManager>();
    }

    void Update()
    {
        
    }

    public void TriggerLetter() {
        if (!collected) {
            bool shouldCollect = solverManager.CollectLetter(letter);
            if (shouldCollect) {
                collected = true;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            TriggerLetter();
        }
    }

    public void SetLetter(char l) {
        letter = l;
        GetComponentInChildren<TextMeshPro>().text = l.ToString();
    }

    public void SetFont(TMP_FontAsset font) {
        GetComponentInChildren<TextMeshPro>().font = font;
    }
}
