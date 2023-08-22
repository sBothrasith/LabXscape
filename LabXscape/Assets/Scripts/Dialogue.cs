using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] dialogueText;
    public float textSpeed;

    private int index;
    public Image image;

    public bool dialogueActive = false;

    void Awake(){
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerControllerMovement>().enabled = false;
        
        image = GetComponent<Image>();

        HideImage();
    }
    // Start is called before the first frame update
    void Start()
    {
        text.text = string.Empty;
        StartText();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if(dialogueActive){
            player.GetComponent<PlayerControllerMovement>().enabled = false;
        }
        else {
            player.GetComponent<PlayerControllerMovement>().enabled = true;
        }
    }

    void StartText() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void HideImage(){
        image.enabled = false;
    }

    void ShowImage(){
        image.enabled = true;
    }

    IEnumerator TypeLine() {
        GameObject player = GameObject.FindWithTag("Player");

        yield return new WaitForSeconds(1);
        ShowImage();
        
        if(player == null) {
            while (index < dialogueText.Length) {
                foreach (char c in dialogueText[index]) {
                    text.text += c;
                    yield return new WaitForSeconds(textSpeed);
                }
                yield return new WaitForSeconds(1);
                text.text = string.Empty;
                index++;
            }
            this.gameObject.SetActive(false);
        }else {
            while (index < dialogueText.Length) {
                dialogueActive = true;
                foreach (char c in dialogueText[index]) {
                    text.text += c;
                    yield return new WaitForSeconds(textSpeed);
                }
                yield return new WaitForSeconds(1);
                text.text = string.Empty;
                index++;
            }
            dialogueActive = false;
            player.GetComponent<PlayerControllerMovement>().enabled = true;
            this.gameObject.SetActive(false);
         }
    }

}
