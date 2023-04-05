using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataObjects;
    private FileDataHandler fileHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake() {
        if(instance != null) {
            Debug.Log("More than one instance");
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.fileHandler = new FileDataHandler(fileName);
        this.dataObjects = FindAllDataObjects();
        LoadGame();
    }

    public void NewGame() {
        this.gameData = new GameData();
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
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataObjects() {
        IEnumerable<IDataPersistence> dataObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataObjects);
    }
}
