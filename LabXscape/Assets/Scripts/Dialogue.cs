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
                dialogueActive = false;
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

        dialogueActive = true;
        yield return new WaitForSeconds(1);
        ShowImage();
        
        foreach (char c in dialogueText[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        
        
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
            //dialogueActive = false;
            this.gameObject.SetActive(false);
        }
    }

}
