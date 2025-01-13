using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueField : MonoBehaviour
{
    public float writingSpeed;

    public TMP_Text dialogueText;
    public string[] dialogues;

    public int index;
    private int charIndex;
    private bool started;
    public bool waitForNext;

    private TMP_Text Answer1;
    private TMP_Text Answer2;
    private GridLayoutGroup gridLayoutGroup;
    public string[] Answers;

    public bool IsDialogueEnded;
    public bool ButtonPressed;
    public bool IsDialogueExit;

    public string[] PhraseEnd1;
    public string[] PhraseEnd2;

    public GameObject NpsWhoInDialogue;

    private void Awake()
    {
        dialogueText.text = string.Empty;
        Answer1 = GameObject.Find("Answer1").GetComponent<TMP_Text>();
        Answer2 = GameObject.Find("Answer2").GetComponent<TMP_Text>();
        gridLayoutGroup = GameObject.Find("PanelAnswerGrid").GetComponent<GridLayoutGroup>();
    }

    public void StartDialogue()
    {
        if (started)
            return;
        started = true;
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        index = i;
        charIndex = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(Writing());
    }

    public void EndDialogue()
    {
        started = false;
        waitForNext = false;
        StopAllCoroutines();
        //dialogueText.text = string.Empty;
    }

    IEnumerator Writing() // правила перебора коллекции строк диалога
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];

        AudioManager2.instance.PlaySFX("char");
        dialogueText.text += currentDialogue[charIndex];

        charIndex++;

        if (charIndex < currentDialogue.Length)
        {
            yield return new WaitForSeconds(writingSpeed);
            StartCoroutine(Writing());
        }
        else
        {
            if (currentDialogue != dialogues[dialogues.Length - 1] || ButtonPressed)
            {
                waitForNext = true;
            }
            else
            {
                IsDialogueEnded = true;
                Answer1.text = Answers[0];
                Answer2.text = Answers[1];
                UpdateSpacing();
                //EndDialogue();
            }
        }
    }

    private void Update()
    {
        if (!started)
            return;

        //if (waitForNext && index + 1 == dialogues.Length && !IsDialogueEnded)
        //{
        //    waitForNext = false;
        //    index++;

        //    GetDialogue(index);
        //    EndDialogue();
        //    IsDialogueEnded = true;
        //    Answer1.text = Answers[0];
        //    Answer2.text = Answers[1];
        //}

        //if (index == dialogues.Length - 1)
        //{
        //    IsDialogueEnded = true;
        //    Answer1.text = Answers[0];
        //    Answer2.text = Answers[1];
        //    UpdateSpacing();
        //    EndDialogue();
        //}

        if (waitForNext && Input.GetKeyDown(KeyCode.E) && !IsDialogueEnded )
        {
            waitForNext = false;
            index++;

            if (index < dialogues.Length)
            {
                GetDialogue(index);
            }

            //else
            //{
            //    IsDialogueEnded = true;
            //    Answer1.text = Answers[0];
            //    Answer2.text = Answers[1];
            //    UpdateSpacing();
            //    EndDialogue();
            //}
        }

        if (waitForNext && Input.GetKeyDown(KeyCode.E) && IsDialogueEnded && ButtonPressed)
        {
            waitForNext = false;
            index++;

            if (index < dialogues.Length)
            {
                GetDialogue(index);
            }
            else
            {
                EndDialogue();
                IsDialogueExit = true;
            }
        }
    }

    public void ClickAnswer1()
    {
        index = 0;
        dialogues = PhraseEnd1;
        ButtonPressed = true;
        StartDialogue();
        GetDialogue(index);
        NpsWhoInDialogue.GetComponent<Romance>().intimacy += 10;
    }

    public void ClickAnswer2()
    {
        index = 0;
        dialogues = PhraseEnd2;
        ButtonPressed = true;
        StartDialogue();
        GetDialogue(index);
        NpsWhoInDialogue.GetComponent<Romance>().intimacy -= 10;
    }

    void UpdateSpacing()
    {
        float preferredHeight1 = Answer1.preferredHeight*1.4f; //lineCount = 7, 28 пикселей одна строка
        float preferredHeight2 = Answer2.preferredHeight * 1.4f;
        float preferredHeight = Math.Max(preferredHeight1, preferredHeight2);
        int lineCount = Mathf.CeilToInt(preferredHeight / Answer1.fontSize);
        float spacing = lineCount * 12.5f;
        gridLayoutGroup.spacing = new Vector2(gridLayoutGroup.spacing.x, 5);
        gridLayoutGroup.cellSize = new Vector2(gridLayoutGroup.cellSize.x, lineCount * 12.5f);
    }
}
