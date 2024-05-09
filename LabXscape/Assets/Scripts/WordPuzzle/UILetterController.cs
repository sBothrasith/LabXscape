using System.Collections;
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
            ("យន្ត", "robot")
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

        List<(char letter, bool collected)> chracterToCollect = wordPuzzleSolveManager.GetCharacterToCollectList();

        if (chracterToCollect != null && chracterToCollect.Count > 0) {
            string word = GetWordFromChars(chracterToCollect);
            ImageLetter(word);

            for (int i = 0; i < chracterToCollect.Count; i++) {
                Instantiate(elementPrefab, container);
            }
        }
    }

    public void UpdateUILetter () {
        List<(char letter, bool collected)> chracterToCollect = wordPuzzleSolveManager.GetCharacterToCollectList();

        for (int i = 0; i < chracterToCollect.Count; i++) {
            if (chracterToCollect[i].collected) {
                if (PlayerPrefs.GetString("language") == "khmer") {
                    container.GetChild(i + 1).GetComponentInChildren<TextMeshProUGUI>().font = wordPuzzleManager.fontKhmerAsset;
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
        Debug.Log(word);
        if (PlayerPrefs.GetString("language") == "khmer") {
            word = MapKhmerToEnglishImage(word);
        }
        Debug.Log(word);
        Sprite image = Resources.Load<Sprite>("HintImage/" + word);
        GameObject hintImage = Instantiate(picturePrefab, container);
        hintImage.GetComponent<Image>().sprite = image;
        ShowPuzzleImage(image);
    }

    public void ShowPuzzleImage(Sprite sprite) {
        puzzleImage.GetComponent<Image>().sprite = sprite;
    }

    private string GetWordFromChars(List<(char letter, bool collected)> word) {
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
