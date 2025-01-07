using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerNoQuest : MonoBehaviour
{
    public DialogueNoQuest dialogueScript; // для получения параметров из скрипта
    private bool playerDetected;

    //Игрок попадает в зону триггера
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Игрок в зоне триггера, отображаем индикатор диалога
        if (collision.tag == "Player")
        {
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Вышли из триггера, прячем индикатор
        if (collision.tag == "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();
        }
    }
    //В зоне триггера, запуск диалога (нажатие E)
    private void Update()
    {

        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            dialogueScript.StartDialogue();
        }
    }
}
