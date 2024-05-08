using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WordPuzzleManager : MonoBehaviour
{
    List<string> words = new() {
        "ROBOT",
        "STEEL",
        "CHAIN",
        "COMPUTER",
        "PLATFORM"
    };
    [Range(1, 3)]
    public int numberOfWordToSolve;

    public Transform startingPoint;
    public LetterPuzzle letterPuzzle;

    public PolygonCollider2D randomSpawnArea;

    private ArrayList wordListToSolve = new();
    private ArrayList letterToSpawn = new();

    public float objectSize = 2f;
    private List<Vector2> spawnedPoints = new List<Vector2>();

    // Start is called before the first frame update
    void Awake()
    {
        randomSpawnArea = randomSpawnArea.GetComponent<PolygonCollider2D>();
        GetRandomWord();
        GenerateLetterForWord();

        foreach(char l in letterToSpawn) {
            SpawnLetter(l, GetRandomPointInCollider(randomSpawnArea));
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
            foreach(char letter in word) {
                letterToSpawn.Add(letter);
            }
        }
    }

    private void SpawnLetter(char letter, Vector2 randomPointToSpawn) {
        foreach (Vector2 point in spawnedPoints) {
            if (Vector2.Distance(randomPointToSpawn, point) < objectSize) {
                do {
                    randomPointToSpawn = GetRandomPointInCollider(randomSpawnArea);
                } while (Vector2.Distance(randomPointToSpawn, point) < objectSize);
            }
        }
        
        LetterPuzzle letterObject = Instantiate(letterPuzzle, new Vector3(randomPointToSpawn.x, randomPointToSpawn.y, startingPoint.position.z), startingPoint.rotation);
        
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
}
