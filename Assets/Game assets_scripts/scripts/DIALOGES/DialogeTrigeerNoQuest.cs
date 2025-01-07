using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerNoQuest : MonoBehaviour
{
    public DialogueNoQuest dialogueScript; // ��� ��������� ���������� �� �������
    private bool playerDetected;

    //����� �������� � ���� ��������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����� � ���� ��������, ���������� ��������� �������
        if (collision.tag == "Player")
        {
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //����� �� ��������, ������ ���������
        if (collision.tag == "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();
        }
    }
    //� ���� ��������, ������ ������� (������� E)
    private void Update()
    {

        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            dialogueScript.StartDialogue();
        }
    }
}
