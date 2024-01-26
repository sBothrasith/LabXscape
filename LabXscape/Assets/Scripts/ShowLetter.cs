using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLetter : MonoBehaviour
{
    private SpriteRenderer letterSprite;
    private Letter letter;
    private bool collected = false;

    private void Awake() {
        letterSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetLetter(Letter l) {
        letter = l;
        SetSprite(letter.GetSprite());
    }

    public void SetSprite(Sprite sprite) {
        letterSprite.sprite = sprite;
    }

    public void TriggerLetter() {
        if (!collected) {
            WordPuzzleSolveManager solverManager = FindObjectOfType<WordPuzzleSolveManager>();
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

}
