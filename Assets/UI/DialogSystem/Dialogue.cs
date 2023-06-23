using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;

    public int index;

    private bool lineDone = false;

    [SerializeField]
    LocalPlayer player1, player2;

    private void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }
    
    void Update()
    {
        
        if (player1.desiresJump || player2.desiresJump)
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
        lineDone = false;

        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        lineDone = true;
    }

 

    void NextLine()
    {
        if (!lineDone) return;


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
