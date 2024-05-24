using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TMP_FontAsset khmerFont;
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    public GameObject skipText;

    private int index;
    public Image image;

    public bool dialogueActive = false;
    public int dialogueCount;

    private string[] dialogueText;

    void Awake(){
        int stageNumber = SceneManager.GetActiveScene().buildIndex;
        dialogueText = DialogueText.GetDialogueFromStage(stageNumber - 1);
        image = GetComponent<Image>();

        HideImage();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("language") == "khmer") {
            textComponent.font = khmerFont;
        }

        dialogueCount = 1;
        textComponent.text = string.Empty;
        skipText.SetActive(false);
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueText.Length > 0) {
            if (dialogueCount >= dialogueText.Length)
            {
                dialogueActive = false;
            }
            else
            {
			    dialogueActive = true;
		    }

            if(textComponent.text == dialogueText[index])
            {
                skipText.SetActive(true);
            }

		    if (Input.GetKeyDown(KeyCode.Return))
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
    }

    void StartDialogue() {
        index = 0;
        if (dialogueText.Length > 0) {
            ShowImage();
            StartCoroutine(TypeLine());
        }
    }

    void HideImage(){
        image.enabled = false;
    }

    void ShowImage(){
        image.enabled = true;
    }

    IEnumerator TypeLine() {
        if (PlayerPrefs.GetString("language") == "khmer") {
            textComponent.text = dialogueText[index];
        } else {
            foreach (char c in dialogueText[index].ToCharArray()) {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

		yield return new WaitForSeconds(3);
		StartCoroutine(TypeLine());
		
	}

    void NextLine()
    {
        skipText.SetActive(false);

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
