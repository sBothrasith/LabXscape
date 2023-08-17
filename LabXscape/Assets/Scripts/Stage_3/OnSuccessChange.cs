using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class OnSuccessChange : MonoBehaviour
{
    public Stage3Puzzle stage3PuzzleManager;

    public Light2D lightColor;

    public SpriteRenderer spriteRenderer;
    public Sprite successSprite;

    Color colorRed = Color.red;
    Color colorGreen = Color.green;
    // Start is called before the first frame update
    void Start()
    {
        lightColor = lightColor.GetComponent<Light2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lightColor.color = colorRed;
        if(stage3PuzzleManager.success){
            lightColor.color = colorGreen;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = successSprite;
        }
    }
}
