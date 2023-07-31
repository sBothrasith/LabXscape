using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] dialogueText;
    public float textSpeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        text.text = string.Empty;
        StartText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartText() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerControllerMovement>().enabled = false;
        while(index < dialogueText.Length) {
            foreach (char c in dialogueText[index]) {
                text.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            yield return new WaitForSeconds(3);
            text.text = string.Empty;
            index++;
        }
        player.GetComponent<PlayerControllerMovement>().enabled = true;
        this.gameObject.SetActive(false);
    }

}
