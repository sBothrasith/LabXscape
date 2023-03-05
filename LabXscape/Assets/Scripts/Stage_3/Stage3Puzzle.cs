using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stage3Puzzle : MonoBehaviour
{
    public bool[] lightTrigger;

    public GameObject movingPlatform;
    void Start()
    {
        lightTrigger = new bool[8];
    }

    // Update is called once per frame
    void Update()
    {
        CheckSolution();
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
        }


    }
}
