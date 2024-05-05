using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILetterController : MonoBehaviour
{
    public GameObject picturePrefab;
    public GameObject puzzleImage;
    public GameObject elementPrefab;
    public Transform container;
    public WordPuzzleManager wordPuzzleManager;
    public WordPuzzleSolveManager wordPuzzleSolveManager;
    public GameObject checkBox;

    private ArrayList letterToSolve;
    public int wordNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        letterToSolve = wordPuzzleManager.getWordListToSolve();
        UpdateUIContainer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUIContainer () {
        foreach (Transform child in container) {
            Destroy(child.gameObject);
        }

        if (letterToSolve != null && letterToSolve.Count > 0) {
            string word = letterToSolve[wordNumber] as string;
            ImageLetter(word);
            for (int i = 0; i < word.Length; i++) {
                Instantiate(elementPrefab, container);
            }
        }
    }

    public void UpdateUILetter () {
        List<Letter> letterCollected = wordPuzzleSolveManager.GetScriptableLetter();

        for (int i = 0; i < letterCollected.Count; i++) {
            container.GetChild(i+1).GetComponent<Image>().sprite = letterCollected[i].letterSrpite;
        }
    }

    public void AddCheckBox() {
        Instantiate(checkBox, container);
    }

    public void ClearContainer() {
        foreach (Transform child in container) {
            Destroy(child.gameObject);
        }
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void ImageLetter (string word) {
        Sprite image = Resources.Load<Sprite>("HintImage/" + word);
        GameObject hintImage = Instantiate(picturePrefab, container);
        hintImage.GetComponent<Image>().sprite = image;
        ShowPuzzleImage(image);
    }

    public void ShowPuzzleImage(Sprite sprite) {
        puzzleImage.GetComponent<Image>().sprite = sprite;
    }
}
