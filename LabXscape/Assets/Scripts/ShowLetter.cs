using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLetter : MonoBehaviour
{
    private SpriteRenderer letterSprite;

    private void Start() {
        letterSprite = gameObject.GetComponent<SpriteRenderer>();
    }


    public GameObject SetSprite(Sprite sprite) {
        letterSprite.sprite = sprite;
        return gameObject;
    }

}
