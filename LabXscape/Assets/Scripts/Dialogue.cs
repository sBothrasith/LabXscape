using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] dialogueText;
    public float textSpeed;

    private int index;
    public Image image;

    public bool dialogueActive = false;

    void Awake(){
        image = GetComponent<Image>();

        HideImage();
    }
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == dialogueText[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = dialogueText[index];
            }
        }   
    }

    void StartDialogue() {
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
        //GameObject player = GameObject.FindWithTag("Player");

        yield return new WaitForSeconds(1);
        ShowImage();
        
        foreach (char c in dialogueText[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        //if (player == null)
        //{
        //    while (index < dialogueText.Length)
        //    {
        //        yield return new WaitForSeconds(1);
        //        textComponent.text = string.Empty;
        //        index++;
        //    }
        //    this.gameObject.SetActive(false);
        //}
        //else
        //{
        //    while (index < dialogueText.Length)
        //    {
        //        dialogueActive = true;
        //        foreach (char c in dialogueText[index])
        //        {
        //            textComponent.text += c;
        //            yield return new WaitForSeconds(textSpeed);
        //        }
        //        yield return new WaitForSeconds(1);
        //        textComponent.text = string.Empty;
        //        index++;
        //    }
        //    dialogueActive = false;
        //    this.gameObject.SetActive(false);
        //}
    }

    void NextLine()
    {
        if (index < dialogueText.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

}
