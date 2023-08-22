using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public PuzzleManager puzzle;
    public GameObject requireText_C, requireText_A, requireText_T, requireText_wrongOrder, wrongInteraction;
    public CinemachineVirtualCamera playerCamera;
    public bool playerIsOnPC1 = false, playerIsOnPC2 = false, playerIsOnPC3 = false;
    public bool keyPress = false;
    private float cameraCurrent, cameraTarget = 0f;
    public float zoomSpeed;
    public float zoomSize;
    public float targetZoomSize;
    // Start is called before the first frame update
    void Start()
    {
        puzzle.interactionFirst = false;
        puzzle.interactionSecond = false;
        puzzle.interactionThird = false;

        puzzle.lightA_Active = false;
        puzzle.lightB_Active = false;
        puzzle.lightC_Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        cameraCurrent = Mathf.MoveTowards(cameraCurrent, cameraTarget, zoomSpeed * Time.deltaTime);
        if (!playerIsOnPC1)
        {
            wrongInteraction.SetActive(false);
            requireText_C.SetActive(false);
        }

        if (!playerIsOnPC2)
        {
            wrongInteraction.SetActive(false);
            requireText_A.SetActive(false);
        }

        if (!playerIsOnPC3)
        {
            wrongInteraction.SetActive(false);
            requireText_T.SetActive(false);
            requireText_wrongOrder.SetActive(false);
        }

        // ---------------------- Check Interaction Message -------------------------------------

        if (Input.GetKeyDown(KeyCode.F))
        {
            keyPress = true;
        }
        else if(Input.GetKeyUp(KeyCode.F)){
            keyPress = false;
        }

        if (playerIsOnPC1  && keyPress)
        {
            puzzle.interactionFirst = true;
            puzzle.lightA_Active = true;
            requireText_C.SetActive(true);
        }

        else if (playerIsOnPC2  && keyPress)
        {
            if (puzzle.interactionFirst)
            {
                puzzle.interactionSecond = true;
                puzzle.lightB_Active = true;
                requireText_A.SetActive(true);
            }
            else
            {
                wrongInteraction.SetActive(true);
            }
        }

        else if (playerIsOnPC3  && keyPress)
        {
            if(puzzle.interactionFirst && puzzle.interactionSecond)
            {
                puzzle.interactionThird = true;
                puzzle.lightC_Active = true;
                requireText_T.SetActive(true);
                cameraTarget = 1;
            }
            else if (puzzle.interactionFirst && !puzzle.interactionSecond)
            {
                puzzle.interactionFirst = false;
                puzzle.lightA_Active = false;
                requireText_wrongOrder.SetActive(true);
            }
            else if(!puzzle.interactionFirst || !puzzle.interactionSecond)
            {
                wrongInteraction.SetActive(true);
            }
        }
        // --------------------------------------------------------------------------------------------
        if (playerCamera.m_Lens.OrthographicSize == targetZoomSize) {
            cameraTarget = 0;
        }
        playerCamera.m_Lens.OrthographicSize = Mathf.Lerp(zoomSize, targetZoomSize, cameraCurrent);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stage2PC1"))
        {
            playerIsOnPC1 = true;
        }
        if (collision.gameObject.CompareTag("Stage2PC2"))
        {
            playerIsOnPC2 = true;
        }
        if (collision.gameObject.CompareTag("Stage2PC3"))
        {
            playerIsOnPC3 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Stage2PC1"))
        {
            playerIsOnPC1 = false;
        }
        if (collision.gameObject.CompareTag("Stage2PC2"))
        {
            playerIsOnPC2 = false;
        }
        if (collision.gameObject.CompareTag("Stage2PC3"))
        {
            playerIsOnPC3 = false;
        }
    }
}
