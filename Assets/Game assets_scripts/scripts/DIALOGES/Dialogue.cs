using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    //���� �������
    public GameObject window;
    //��������� �������
    public GameObject indicator;
    //�����
    public TMP_Text dialogueText;
    //������ ����� �������
    public List<string> dialogues;


    //�������� ��������� ����
    public float writingSpeed;
    //������ ������ �������
    private int index;
    //������ ����� �������
    private int charIndex;
    //������ �������, ���������� ��������
    private bool started;
    //��������� ������ �������
    private bool waitForNext;

    public bool NPCMissionDone;
    public GameObject NPC;

    //�������� ���� ���������� � �������
    private void Awake()
    {
        dialogueText.text = string.Empty;
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    //���� ������� (���������)
    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    //��������� ������� (���������)
    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }


    //������ �������
    public void StartDialogue()
    {
        if (started) //��������, ���������� �������
            return;
        if (NPCMissionDone)
        {
            //������ �������
            started = true;
            //������ ����� ������� �������
            ToggleWindow(true);
            //������ ���������
            ToggleIndicator(false);
            //������ ������ �������, � ������� �������� ������
            GetDialogue(0);
        }
    }

    private void GetDialogue(int i)
    {
        //�������� ������ � ������� 0
        index = i;
        //������ ��������
        charIndex = 0;
        //�������� ����� ����������� ����, ����� �������� ����� ������
        dialogueText.text = string.Empty;
        //Start writing
        StartCoroutine(Writing());
    }

    //End Dialogue
    public void EndDialogue()
    {
        //������ ������� = ����
        started = false;
        //��������� �������� ����. ������
        waitForNext = false;
        //������������� Ienumerators
        StopAllCoroutines();
        dialogueText.text = string.Empty;
        //������ ����
        ToggleWindow(false);
    }
    //������ ��������� ������
    IEnumerator Writing() // ������� �������� ��������� ����� �������
    {
        //�������, ������� ��������� ����������� ��������� ��������
        yield return new WaitForSeconds(writingSpeed);

        //������ ����� �������
        string currentDialogue = dialogues[index];
        //����� ������ �� ����� �����
        AudioManager2.instance.PlaySFX("char");
        dialogueText.text += currentDialogue[charIndex];
        //�������� ������ �����
        charIndex++;
        //�������� �� ����� �����������
        if (charIndex < currentDialogue.Length)
        {
            //������� ��� �� ��������� ������� 
            yield return new WaitForSeconds(writingSpeed);
            //���������� ������  //��� �������, ������� ����� ������������� ���������� � ������� ���������� Unity, � ����� ���������� ������ � ���� �����, �� ������� ������������, � ��������� �����.
            StartCoroutine(Writing());
        }
        else
        {
            //��������� �� ��������� ������
            waitForNext = true;
        }
    }

    private void Update()
    {
        NPCMissionDone = NPC.GetComponent<MissionBot>().MissionDone;

        if (!started)
            return;

        //��������:  ��������� ������ � ���� ������� E
        if (waitForNext && Input.GetKeyDown(KeyCode.E) && NPCMissionDone)
        {
            //������� �� ��������� ������
            waitForNext = false;
            index++;

            //�������� ������ �� ������ �����
            if (index < dialogues.Count)
            {
                //��������� �� ����. ������
                GetDialogue(index);
            }
            else
            {
                // ������ ��������
                ToggleIndicator(true);
                EndDialogue();
            }
        }
    }
}
