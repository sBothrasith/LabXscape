using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WordPuzzleManager : MonoBehaviour
{
    List<string> englishWords = new() {
        "STEEL",
        "ROBOT",
        "CHAIN",
        "COMPUTER",
        "PLATFORM",
        "DOOR",
        "BUTTON",
        "LASER",
        "PIPE",
        "MONITOR",
        "LADDER",
        "WIRE",
        "BARREL",
        "ELEVATOR",
        "BARRIER",  
        "BRICK",
        "LETTER",
        "SIGN",
        "WOODENBOX",
        "TRASHBIN",
        "VENTILATION",
        "LABORATORY",
        "FLASHLIGHT",
        "COMPASS",
        "BINOCULARS",
        "HOSE",
        "PLANK",
        "BANDAGE",
        "SPONGE"
    };

    List<string> khmerWords = new() {
        //"ែដក",
        //"មនុស្សយន្ត",
        //"្រចវ៉ាក់",
        ////"កុំព្យូទ័រ",
        //"ក្តារ",
        //"ទ្វា",
        //"ប៊ូតុង",
        //"ឡាែស៊រ",
        //"បំពង់",
        //"ម៉ូនីទ័រ",
        ////"ជណ្តើរ",
        //"ែខ្ស",
        //"ធុង",
        ////"ជណ្តើរយន្ត",
        "របាំង",
        //"ឥដ្ឋ",
        //"អក្សរ",
        //"សញ្ញា",
        ////"ប្រអប់ឈើ",
        //"ធុងសំរាម",
        //"រន្ធខ្យល់",
        //"មន្ទីរ",
        //"ពិល",
        //"្រតីវិស័យ",
        //"ែកវយឹត",
        ////"ទុយោ",
        ////"បន្ទះឈើ",
        ////"បង់រុំ",
        //"េអប៉ុង"
    };

    List<string> words = new();

    [Range(1, 3)]
    public int numberOfWordToSolve;

    public Transform startingPoint;
    public LetterPuzzle letterPuzzle;

    public PolygonCollider2D randomSpawnArea;
    public TMP_FontAsset fontKhmerAsset;

    private ArrayList wordListToSolve = new();
    private Dictionary<string, List<string>> wordletterToSolve = new();

    public float objectSize = 2f;
    private List<Vector2> spawnedPoints = new List<Vector2>();

    public static char ConsonantJoiner = '្';

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetString("language") == "khmer") {
            words = khmerWords;
        } else {
            words = englishWords;
        }


        randomSpawnArea = randomSpawnArea.GetComponent<PolygonCollider2D>();
        GetRandomWord();
        GenerateLetterForWord();

        foreach (List<string> wordLetter in wordletterToSolve.Values) {
            foreach (string l in wordLetter) {
                SpawnLetter(l, GetRandomPointInCollider(randomSpawnArea));
            }
        }
    }

    private void GetRandomWord() {
        for (int i = 0; i < numberOfWordToSolve; i++) {
            int index = Random.Range(0, words.Count);
            if (wordListToSolve.Contains(words[index])) {
                words.Remove(words[index]);
                int newIndex = Random.Range(0, words.Count);
                wordListToSolve.Add(words[newIndex]);
            }
            else {
                wordListToSolve.Add(words[index]);
            }
        }
    }

    private void GenerateLetterForWord() {
        foreach(string word in wordListToSolve) {
            List<string> letter = new List<string>();
            for(int i = 0; i < word.Length; i++) {
                if (word[i] == ConsonantJoiner) {
                    string conLetter = word[i] + word[i+1].ToString();
                    letter.Add(KhmerFontAdjuster.Adjust(conLetter));
                    i++;
                    continue;
                }
                letter.Add(word[i].ToString());
            }
            string wordToStore = word;
            if (PlayerPrefs.GetString("language") == "khmer") {
                wordToStore = KhmerFontAdjuster.Adjust(word);
            }
            wordletterToSolve[wordToStore] = letter;
        }
    }

    private void SpawnLetter(string letter, Vector2 randomPointToSpawn) {
        foreach (Vector2 point in spawnedPoints) {
            if (Vector2.Distance(randomPointToSpawn, point) < objectSize) {
                do {
                    randomPointToSpawn = GetRandomPointInCollider(randomSpawnArea);
                } while (Vector2.Distance(randomPointToSpawn, point) < objectSize);
            }
        }
        
        LetterPuzzle letterObject = Instantiate(letterPuzzle, new Vector3(randomPointToSpawn.x, randomPointToSpawn.y, startingPoint.position.z), startingPoint.rotation);
        
        
        if (PlayerPrefs.GetString("language") == "khmer") {
            letterObject.SetFont(fontKhmerAsset);
        }
        
        letterObject.SetLetter(letter);

        spawnedPoints.Add(randomPointToSpawn);
		
	}

    private Vector2 GetRandomPointInCollider(PolygonCollider2D collider) {
        Vector2 randomPoint;
        do {
            randomPoint = new Vector2(Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                                      Random.Range(collider.bounds.min.y, collider.bounds.max.y));
        } while (!collider.OverlapPoint(randomPoint));

        return randomPoint;
    }

    public ArrayList getWordListToSolve () {
        return wordListToSolve;
    }

    public Dictionary<string, List<string>> GetLetterListToSolve() {
        return wordletterToSolve;
    }
}
