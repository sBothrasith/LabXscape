using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElevatorTrigger : MonoBehaviour
{

    bool elevatorTrigger = false;

    public TextMeshPro nextText;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nextText.enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            elevatorTrigger = true;
            animator.SetBool("ElevatorTrigger", elevatorTrigger);
            nextText.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            elevatorTrigger = false;
            animator.SetBool("ElevatorTrigger", elevatorTrigger);
            nextText.enabled = false;
        }
    }
}
