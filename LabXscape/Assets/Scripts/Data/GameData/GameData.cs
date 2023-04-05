using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentScene;
    public Vector3 playerPosition;

    public GameData() {
        this.currentScene = 1;
        this.playerPosition = new Vector3(-8.801409f, -2.07106f, 0.0f);
    }

}
