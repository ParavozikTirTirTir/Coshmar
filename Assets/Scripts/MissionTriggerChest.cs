using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class MissionTriggerChest : MonoBehaviour
{
    public bool trigger = false;

    private MissionChest MC; // подключаем скрипт MissionBot чтобы брать оттуда состояние диалога;

    void Start()
    {
        MC = GetComponent<MissionChest>(); //
    }

    void OnTriggerStay2D(Collider2D obj) //«Наезд» на объект
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //анти наезд на объект
    {
        if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void OnGUI() //кнопка поговорить
    {
        if (trigger && MC.MissionDone == false)
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 110, 25), "[Е] Открыть");
        }
    }
}
