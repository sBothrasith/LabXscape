using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="WordPuzzle/Letter")]
public class Letter : ScriptableObject
{

    public char letter;
    public Sprite letterSrpite;

    public Letter GetObject() {
        return this;
    }

    public char GetLetter() {
        return letter;
    }

    public Sprite GetSprite() {
        return letterSrpite;
    }
}
