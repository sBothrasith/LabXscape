using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="WordPuzzle/Letter")]
public class Letter : ScriptableObject
{

    public char letter;
    public Sprite letterSrpite;


    public Sprite GetSprite() {
        return letterSrpite;
    }
}
