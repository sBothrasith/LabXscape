using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorChange : MonoBehaviour
{
    public WordPuzzleSolveManager worldPuzzleManager;

    public GameObject LightA, LightB, LightC;
    private Light2D lightChangeA;
	private Light2D lightChangeB;
	private Light2D lightChangeC;

    Color colorRed = Color.red;
    Color colorGreen = Color.green;

    private void Start()
    {
        LightA.GetComponent<Light2D>().color = colorRed;
        LightB.GetComponent<Light2D>().color = colorRed;
        LightC.GetComponent<Light2D>().color = colorRed;

	}
    // Update is called once per frame
    void Update()
    {

        if (worldPuzzleManager.solved)
        { 
			LightA.GetComponent<Light2D>().color = colorGreen;
			LightB.GetComponent<Light2D>().color = colorGreen;
			LightC.GetComponent<Light2D>().color = colorGreen;
		}
    }

            
}
