using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMoveScript : MonoBehaviour
{
    [SerializeField]int sceneBuildIndex;
    private bool playerConfirm = false;
    public LevelLoader levelLoader;

	private const string _Stage_One = "Stage_1";

	public void SkipTutorial()
	{
		SceneManager.LoadScene(_Stage_One);
	}

	private void Start() {
        levelLoader = levelLoader.GetComponent<LevelLoader>();
    }

    private void Update()
    {
        if(playerConfirm && Input.GetKeyDown(KeyCode.W))
        {
            print("Loading next scene");
            levelLoader.StartLoadScene = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Player Entered");

        if (collision.CompareTag("Player"))
        {
            playerConfirm = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Player leaved");

        if (collision.CompareTag("Player"))
        {
            playerConfirm = false;
        }
    }
}
