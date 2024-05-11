using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string dataPath;
    private readonly string encryptionCodeWord = "lab";
    private const int _StartStage = 1;
    private void Awake() {
        dataPath = Application.persistentDataPath;
    }
    public void NewGame() {
        Debug.Log("NewStart");
        DataPersistenceManager.instance.NewGame();
        DataPersistenceManager.instance.SaveGame();
        Debug.Log("Hi");
        SceneManager.LoadScene(1);
    }
    
    public void ContinueGame() {
        string fullPath = Path.Combine(dataPath, DataPersistenceManager.instance.fileName);
        Debug.Log(fullPath);
        if(File.Exists(fullPath)) {
            try {
                string dataLoad = "";

                using(FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                    using(StreamReader reader = new StreamReader(stream)) {
                        dataLoad = reader.ReadToEnd();
                    }
                }

                dataLoad = EncryptDecrypt(dataLoad);

                GameData loadedData = JsonUtility.FromJson<GameData>(dataLoad);
                SceneManager.LoadScene(loadedData.currentScene);

            }catch(Exception e) {
                Debug.LogError("Error when continuing file: " + fullPath + "\n" + e);
            }
        }
    }

    public void QuitGame() {
        Application.Quit();
    }

    private string EncryptDecrypt(string data) {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++) {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }

        return modifiedData;
    }
}
