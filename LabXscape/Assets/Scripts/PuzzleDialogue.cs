using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PuzzleDialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string dialogueText;
    public float textSpeed;

    private int index;
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

    IEnumerator TypeLine()
    {
        foreach (char c in dialogueText)
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
