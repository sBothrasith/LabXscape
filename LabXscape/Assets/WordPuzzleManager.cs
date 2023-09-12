using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WordPuzzleManager : MonoBehaviour
{
    List<string> words = new() {
        "laser",
        "robot",
        "metal",
        "stone",
        "chain",
        "platform",
        "dog"
    };
    [Range(1, 3)]
    public int numberOfWordToSolve;

    public Transform startingPoint;
    public Letter t;
    public PolygonCollider2D randomSpawnArea;

    private ArrayList wordListToSolve = new();
    private ArrayList letterToSpawn = new();
    private ArrayList scriptableObjectLetter = new();

    private Sprite[] spriteLetterToSpawn;
    public ShowLetter showLetter;
    public Letter[] letter;
    public float objectSize = 2f;
    private List<Vector2> spawnedPoints = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        randomSpawnArea = randomSpawnArea.GetComponent<PolygonCollider2D>();
        GetRandomWord();
        GenerateLetterForWord();
        spriteLetterToSpawn = new Sprite[letterToSpawn.Count];
        FromLetterToScriptableObject();
        foreach(Sprite l in spriteLetterToSpawn) {
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
                spriteLetterToSpawn[i] = matchLetter.GetSprite();
            }
        }
    }

    private void SpawnLetter(Sprite sprite, Vector2 randomPointToSpawn) {
        foreach (Vector2 point in spawnedPoints) {
            if (Vector2.Distance(randomPointToSpawn, point) < objectSize) {
                // Generate a new random point until it is not too close to any existing points
                do {
                    randomPointToSpawn = GetRandomPointInCollider(randomSpawnArea);
                } while (Vector2.Distance(randomPointToSpawn, point) < objectSize);
            }
        }
        Instantiate(showLetter.SetSprite(sprite), new Vector3(randomPointToSpawn.x, randomPointToSpawn.y, startingPoint.position.z), startingPoint.rotation);
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

}
