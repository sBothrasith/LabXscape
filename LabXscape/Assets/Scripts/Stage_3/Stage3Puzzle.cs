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

    public GameObject card;

    void Start()
    {
        lightTrigger = new bool[8];
        success = false;
		card.SetActive(false);

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
			card.SetActive(true);
			StartCoroutine(WaitForSecs());
		}
    }

    IEnumerator WaitForSecs()
    {
        yield return new WaitForSeconds(1.5f);
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
