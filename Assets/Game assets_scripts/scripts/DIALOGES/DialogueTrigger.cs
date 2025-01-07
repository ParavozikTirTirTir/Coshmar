using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript; // ��� ��������� ���������� �� �������
    private bool playerDetected;
    public bool NPCMissionDone;
    public GameObject NPC;

    //����� �������� � ���� ��������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����� � ���� ��������, ���������� ��������� �������
        if (collision.tag == "Player" && NPCMissionDone)
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
        NPCMissionDone = NPC.GetComponent<MissionBot>().MissionDone;

        if (playerDetected && Input.GetKeyDown(KeyCode.E) && NPCMissionDone)
        {
            dialogueScript.StartDialogue();
        }
    }
}
