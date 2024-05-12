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
            if (l.Contains("\u17CB")) {
                textMesh.margin = new Vector4(10.86f, 3.43f, textMesh.margin.z, textMesh.margin.w);
            }
            else if (l.Contains("3") || l.Contains("8") || l.Contains("13") || l.Contains("20") || l.Contains("25") || l.Contains("26") || l.Contains("30")) {
                textMesh.fontSize = 19;
                textMesh.margin = new Vector4(9.8f, 0.8f, textMesh.margin.z, textMesh.margin.w);
            }
            else if (l.Contains("sprite")) {
                textMesh.margin = new Vector4(10.55f, -0.02f, textMesh.margin.z, textMesh.margin.w);
            }
        }
        textMesh.text = l;
    }

    public void SetFont(TMP_FontAsset font) {
        GetComponentInChildren<TextMeshPro>().font = font;
    }
}
