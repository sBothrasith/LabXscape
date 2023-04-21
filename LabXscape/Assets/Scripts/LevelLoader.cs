using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour, IDataPersistence
{
    public Animator transition;

    public float transitionTime = 1f;
    
    public bool StartLoadScene = false;
    public int currentScene = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StartLoadScene) {
            LoadNextLevel();
        }
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("LevelLoader");

        yield return new WaitForSeconds(transitionTime);
        StartLoadScene = false;
        SceneManager.LoadScene(levelIndex);

    }

    public void LoadGameData(GameData data) {
        this.currentScene = data.currentScene;
    }

    public void SaveGameData(GameData data) {
        data.currentScene = this.currentScene;
    }

}
