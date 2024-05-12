﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class UILetterController : MonoBehaviour
{
    public GameObject picturePrefab;
    public GameObject puzzleImage;
    public GameObject elementPrefab;
    public Transform container;

    private WordPuzzleManager wordPuzzleManager;
    private WordPuzzleSolveManager wordPuzzleSolveManager;

    List<(string khmerWord, string englishWord)> khmerToEnglish = new() {
            ("ែដក", "steel"),
            ("យន្ត", "robot"),
            ("ចង្វាក់", "chain"),
    };

    // Start is called before the first frame update
    void Start()
    {
        wordPuzzleManager = FindObjectOfType<WordPuzzleManager>();
        wordPuzzleSolveManager = FindObjectOfType<WordPuzzleSolveManager>();

        StartCoroutine(DelayedUpdateUIContainer());
    }

    private IEnumerator DelayedUpdateUIContainer() {
        yield return new WaitForSeconds(0.3f); 
        UpdateUIContainer();
    }

    public void UpdateUIContainer () {
        foreach (Transform child in container) {
            Destroy(child.gameObject);
        }

        List<(string letter, bool collected)> chracterToCollect = wordPuzzleSolveManager.GetCharacterToCollectList();

        if (chracterToCollect != null && chracterToCollect.Count > 0) {
            string word = GetWordFromChars(chracterToCollect);
            ImageLetter(word);

            for (int i = 0; i < chracterToCollect.Count; i++) {
                Instantiate(elementPrefab, container);
            }
        }
    }

    public void UpdateUILetter () {
        List<(string letter, bool collected)> chracterToCollect = wordPuzzleSolveManager.GetCharacterToCollectList();

        for (int i = 0; i < chracterToCollect.Count; i++) {
            if (chracterToCollect[i].collected) {
                if (PlayerPrefs.GetString("language") == "khmer") {
                    TextMeshProUGUI textMesh = container.GetChild(i + 1).GetComponentInChildren<TextMeshProUGUI>();
                    textMesh.font = wordPuzzleManager.fontKhmerAsset;
                    string l = chracterToCollect[i].letter.ToString();
                    if (l.Contains("\u17CB")) {
                        textMesh.margin = new Vector4(98.42f, 11.7f, textMesh.margin.z, textMesh.margin.w);
                    }
                    else if (l.Contains("3") || l.Contains("8") || l.Contains("13") || l.Contains("20") || l.Contains("25") || l.Contains("26") || l.Contains("30")) {
                        textMesh.fontSize = 90;
                        textMesh.margin = new Vector4(99.01f, -64.2f, textMesh.margin.z, textMesh.margin.w);
                    }
                    else if (l.Contains("sprite")) {
                        textMesh.margin = new Vector4(98.6f, -80f, textMesh.margin.z, textMesh.margin.w);
                    }
                }
                container.GetChild(i+1).GetComponentInChildren<TextMeshProUGUI>().text= chracterToCollect[i].letter.ToString();
            }
        }
    }

    public void ClearContainer() {
        foreach (Transform child in container) {
            Destroy(child.gameObject);
        }
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void ImageLetter (string word) {

        if (PlayerPrefs.GetString("language") == "khmer") {
            word = MapKhmerToEnglishImage(word);
        }

        Sprite image = Resources.Load<Sprite>("HintImage/" + word);
        GameObject hintImage = Instantiate(picturePrefab, container);
        hintImage.GetComponent<Image>().sprite = image;
        ShowPuzzleImage(image);
    }

    public void ShowPuzzleImage(Sprite sprite) {
        puzzleImage.GetComponent<Image>().sprite = sprite;
    }

    private string GetWordFromChars(List<(string letter, bool collected)> word) {
        string result = string.Empty;

        foreach (var item in word) {
            result += item.letter;
        }

        return result;
    }

    private string MapKhmerToEnglishImage(string khmerWord) {
        for (int i = 0; i < khmerToEnglish.Count; i++) {
            if (khmerToEnglish[i].khmerWord == khmerWord) {
                return khmerToEnglish[i].englishWord;
            }
        }

        return "";
    }
}
