using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class MissionTrigger : MonoBehaviour
{
    public bool trigger = false;

    private MissionBot MB; // ���������� ������ MissionBot ����� ����� ������ ��������� �������;
    private OpenInventory OI;

    void Start()
    {
        MB = GetComponent<MissionBot>(); //
        OI = GameObject.FindGameObjectWithTag("InvCanvas").GetComponent<OpenInventory>();
    }

    void OnTriggerStay2D(Collider2D obj) //������ �� ������
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //���� ����� �� ������
    {
        if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void OnGUI() //������ ����������
    {
        if (trigger && MB.vis == false && MB.MissionDone == false)
        {
            GUI.Box(new Rect(Screen.width/2 + 20, Screen.height/2 + 40, 110, 25), "[�] ����������");
        }
    }
}