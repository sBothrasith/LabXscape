using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject movingPlatform;
    
    public bool interactionFirst, interactionSecond, interactionThird;
    public bool lightA_Active, lightB_Active, lightC_Active;

    // Update is called once per frame
    void Update()
    {
        CheckSolution();
    }

    private void CheckSolution()
    {
        if (interactionFirst == true && interactionSecond == true && interactionThird == true)
        {
            movingPlatform.SetActive(true);
        }
    }
}
