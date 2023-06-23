using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;

    public int index;

    private void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

 

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
