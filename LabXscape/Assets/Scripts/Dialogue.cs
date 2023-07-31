using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string dialogueText;
    public float textSpeed;


    // Start is called before the first frame update
    void Start()
    {
        text.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator TypeLine() {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerControllerMovement>().enabled = false;
        foreach (char c in dialogueText) {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        player.GetComponent<PlayerControllerMovement>().enabled = true;
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }

}
