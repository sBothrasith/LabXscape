using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] public string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataObjects;
    private FileDataHandler fileHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake() {
        if(instance != null) {
            Debug.Log("More than one instance");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        this.fileHandler = new FileDataHandler(fileName);
        this.gameData = fileHandler.Load();
    }
    private void Start() {
        LoadGame();
    }
    public void NewGame() {
        this.gameData = new GameData();
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        this.dataObjects = FindAllDataObjects();
        if (gameData == null) {
            return;
        }
        if (SceneManager.GetActiveScene().buildIndex == gameData.currentScene) {
            LoadGame();
        }
    }

    public void LoadGame() {
        this.gameData = fileHandler.Load();

        if(this.gameData == null) {
            Debug.Log("No data found. Creating new profile");
            NewGame();
        }
        foreach(IDataPersistence dataObj in dataObjects) {
            dataObj.LoadGameData(gameData);
        }
    }

    public void SaveGame() {
        foreach(IDataPersistence dataObj in dataObjects) {
            dataObj.SaveGameData(gameData);
        }

        fileHandler.Save(gameData);
    }

    public void OnApplicationQuit() {
        this.dataObjects = FindAllDataObjects();
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataObjects() {
        IEnumerable<IDataPersistence> dataObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataObjects);
    }
}
