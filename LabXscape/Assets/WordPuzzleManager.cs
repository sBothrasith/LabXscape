using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WordPuzzleManager : MonoBehaviour
{
    List<string> words = new() {
        "robot",
        "steel",
        "chain",
        "computer",
        "platform"
    };
    [Range(1, 3)]
    public int numberOfWordToSolve;

    public Transform startingPoint;
    public Letter t;
    public PolygonCollider2D randomSpawnArea;

    private ArrayList wordListToSolve = new();
    private ArrayList letterToSpawn = new();
    private ArrayList scriptableObjectLetter = new();

    private Letter[] scriptLetterToSpawn;
    public  GameObject showLetter;
    public Letter[] letter;
    public float objectSize = 2f;
    private List<Vector2> spawnedPoints = new List<Vector2>();
    // Start is called before the first frame update
    void Awake()
    {
        randomSpawnArea = randomSpawnArea.GetComponent<PolygonCollider2D>();
        GetRandomWord();
        GenerateLetterForWord();
        scriptLetterToSpawn = new Letter[letterToSpawn.Count];
        FromLetterToScriptableObject();
        foreach(Letter l in scriptLetterToSpawn) {
            SpawnLetter(l, GetRandomPointInCollider(randomSpawnArea));
        }
    }

    // Update is called once per frame
    void Update()
    {

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
            foreach(char letter in word) {
                letterToSpawn.Add(letter);
            }
        }
    }

    private void FromLetterToScriptableObject() {
        for(int i = 0; i < letterToSpawn.Count; i++) {
            char l = (char)letterToSpawn[i];
            Letter matchLetter = letter.FirstOrDefault(ml => ml.letter == l);
            if(matchLetter != null) {
                scriptLetterToSpawn[i] = matchLetter.GetObject();
            }
        }
    }

    private void SpawnLetter(Letter letter, Vector2 randomPointToSpawn) {
        foreach (Vector2 point in spawnedPoints) {
            if (Vector2.Distance(randomPointToSpawn, point) < objectSize) {
                do {
                    randomPointToSpawn = GetRandomPointInCollider(randomSpawnArea);
                } while (Vector2.Distance(randomPointToSpawn, point) < objectSize);
            }
        }
        
        GameObject letterBox = Instantiate(showLetter, new Vector3(randomPointToSpawn.x, randomPointToSpawn.y, startingPoint.position.z), startingPoint.rotation);
        ShowLetter sLetter = letterBox.AddComponent<ShowLetter>();
        sLetter.SetLetter(letter);

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
}
