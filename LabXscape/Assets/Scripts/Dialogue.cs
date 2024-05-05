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
    public int dialogueCount;

    void Awake(){
        image = GetComponent<Image>();

        HideImage();
    }
    // Start is called before the first frame update
    void Start()
    {
        dialogueCount = 1;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueCount >= dialogueText.Length)
        {
            dialogueActive = false;
        }
        else
        {
			dialogueActive = true;
		}
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
        ShowImage();
		StartCoroutine(TypeLine());
    }

    void HideImage(){
        image.enabled = false;
    }

    void ShowImage(){
        image.enabled = true;
    }

    IEnumerator TypeLine() {

        yield return new WaitForSeconds(1);
        
        foreach (char c in dialogueText[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(3);
		NextLine();
	}

    void NextLine()
    {
        if (index < dialogueText.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
			dialogueCount++;
            StartCoroutine(TypeLine());
		}
		else
        {
            //dialogueActive = false;
            this.gameObject.SetActive(false);
        }
    }

}
