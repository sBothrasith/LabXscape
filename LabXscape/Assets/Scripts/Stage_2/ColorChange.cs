using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorChange : MonoBehaviour
{
    public PuzzleManager puzzle;

    public Light2D light1, light2, light3;

    Color colorRed = Color.red;
    Color colorGreen = Color.green;

    private void Start()
    {
        light1 = GetComponent<Light2D>();
        light2 = GetComponent<Light2D>();
        light3 = GetComponent<Light2D>();

    }
    // Update is called once per frame
    void Update()
    {
        light1.color = colorRed;
        light2.color = colorRed;
        light3.color = colorRed;
        if (puzzle.interactionFirst == true)
        {
            light1.color = colorGreen;
        }
        if (puzzle.interactionSecond == true)
        {
            light2.color = colorGreen;
        }
        if (puzzle.interactionThird == true)
        {
            light3.color = colorGreen;
        }

        else
        {
            light1.color = colorRed;
            light2.color = colorRed;
            light3.color = colorRed;
        }
    }
}
