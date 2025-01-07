using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueField : MonoBehaviour
{
    public TMP_Text dialogueText;
    public List<string> dialogues;

    public float writingSpeed;
    private int index;
    private int charIndex;
    private bool started;
    private bool waitForNext;

    private void Awake()
    {
        dialogueText.text = string.Empty;
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
        dialogueText.text = string.Empty;
    }

    IEnumerator Writing() // правила перебора коллекции строк диалога
    {
        //функци€, котора€ позвол€ет задерживать написание символов
        yield return new WaitForSeconds(writingSpeed);

        //список строк диалога
        string currentDialogue = dialogues[index];
        //пишем тексто по одной букве
        AudioManager2.instance.PlaySFX("char");
        dialogueText.text += currentDialogue[charIndex];
        //измен€ем индекс буквы
        charIndex++;
        //ѕроверка на конец предложени€
        if (charIndex < currentDialogue.Length)
        {
            //сколько ждЄм до написани€ символа 
            yield return new WaitForSeconds(writingSpeed);
            //продолжаем писать  //это функци€, котора€ может приостановить выполнение и вернуть управление Unity, а затем продолжить работу с того места, на котором остановилась, в следующем кадре.
            StartCoroutine(Writing());
        }
        else
        {
            //переходим на следующую строку
            waitForNext = true;
        }
    }

    private void Update()
    {

        if (!started)
            return;

        if (waitForNext && Input.GetKeyDown(KeyCode.E))
        {
            waitForNext = false;
            index++;

            if (index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}
