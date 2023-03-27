using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorChange : MonoBehaviour
{
    public PuzzleManager puzzle;

    public GameObject LightA, LightB, LightC;
    public Light2D lightChangeA;
    public Light2D lightChangeB;
    public Light2D lightChangeC;

    Color colorRed = Color.red;
    Color colorGreen = Color.green;

    private void Start()
    {
        lightChangeA = LightA.GetComponent<Light2D>();
        lightChangeB = LightB.GetComponent<Light2D>();
        lightChangeC = LightC.GetComponent<Light2D>();

    }
    // Update is called once per frame
    void Update()
    {
        lightChangeA.color = colorRed;
        lightChangeB.color = colorRed;
        lightChangeC.color = colorRed;
        if (puzzle.lightA_Active)
        {
            lightChangeA.color = colorGreen;
        }
        if (puzzle.lightA_Active && puzzle.lightB_Active)
        {
            lightChangeB.color = colorGreen;
        }
        if (puzzle.lightA_Active && puzzle.lightB_Active && puzzle.lightC_Active)
        {
            lightChangeC.color = colorGreen;
        }
        
    }

            
}
