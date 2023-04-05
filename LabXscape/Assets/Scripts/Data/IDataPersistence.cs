using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadGameData(GameData data);
    void SaveGameData(GameData data);
}
