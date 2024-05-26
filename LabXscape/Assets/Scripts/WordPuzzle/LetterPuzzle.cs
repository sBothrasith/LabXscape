using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class LetterPuzzle : MonoBehaviour
{
    private bool collected = false;
    private WordPuzzleSolveManager solverManager;
    private string letter;

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

    public void SetLetter(string l) {
        letter = l;
        TextMeshPro textMesh = GetComponentInChildren<TextMeshPro>();
        if (PlayerPrefs.GetString("language") == "khmer") {
            textMesh.fontSize = 42;
        }
        textMesh.text = l;
    }

    public void SetFont(TMP_FontAsset font) {
        GetComponentInChildren<TextMeshPro>().font = font;
    }
}
