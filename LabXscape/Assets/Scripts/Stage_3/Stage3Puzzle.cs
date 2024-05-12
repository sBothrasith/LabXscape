using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stage3Puzzle : MonoBehaviour
{
    public bool[] lightTrigger;
    public GameObject[] Buttons;

    public GameObject movingPlatform;
    public bool success = false;

    void Start()
    {
        lightTrigger = new bool[8];
        success = false;

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckSolution();

        if(success)
        {
            StartCoroutine(WaitForSecs());
		}
    }

    IEnumerator WaitForSecs()
    {
        yield return new WaitForSeconds(4);
		for (int i = 0; i < Buttons.Length; i++)
		{
			Buttons[i].SetActive(false);
		}
	}

    public void setLightTrue(int ID) {
        lightTrigger[ID] = true;
    }

    public void setLightFalse(int ID) {
        lightTrigger[ID] = false;
    }

    private void CheckSolution() {
        bool checkTrigger = lightTrigger.All(l => l == true);
        if (checkTrigger) {
            movingPlatform.SetActive(true);
            success = true;
        }
    }

}
