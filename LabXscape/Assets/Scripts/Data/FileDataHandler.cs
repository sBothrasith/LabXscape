using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataPath = Application.persistentDataPath;
    private string dataFileName = "";

    public FileDataHandler(string dataFileName) {
        this.dataFileName = dataFileName;
    }

    public GameData Load() {
        string fullPath = Path.Combine(dataPath, dataFileName);
        GameData loadedData = null;
        Debug.Log(fullPath);
        if(File.Exists(fullPath)) {
            try {
                string dataLoad = "";

                using(FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                    using(StreamReader reader = new StreamReader(stream)) {
                        dataLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataLoad);

            }catch(Exception e) {
                Debug.LogError("Error when loading file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data) {
        string fullPath = Path.Combine(dataPath, dataFileName);
        Debug.Log(fullPath);
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataStore = JsonUtility.ToJson(data, true);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                using(StreamWriter writer = new StreamWriter(stream)) {
                    writer.WriteLine(dataStore);
                }
            }
        }catch(Exception e ) {
            Debug.LogError("Error when Saving file: " + fullPath + "\n" + e);
        }
    }

}