using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public GameObject movingPlatform;
    public GameObject[] interactionComputer;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PuzzleInteraction()
    {

    }

    private void CheckSolution()
    {
        bool checkTrigger = true;
        if (checkTrigger)
        {
            movingPlatform.SetActive(true);
        }
    }
}
