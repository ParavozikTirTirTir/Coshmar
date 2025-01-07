using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class Romance : MonoBehaviour
{
    public bool trigger = false;
    public bool vis;
    public int intimacy;
    public Texture2D[] Emotions;
    public List<string> Phrases = new List<string>();
    public string CharName;

    public string CharacterInformation; //Информация о миссии, которая выводится при нажатии на миссию в левом верхнем углу

    private MissionManager MM;
    private Inventory Inv;
    private PlayerController PC;
    private PlayerCombatController PCC;
    private IsPlayerInDialoge PinD;

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();
        intimacy = 0;
    }

    void OnTriggerStay2D(Collider2D obj) //игрок рядом с НПС
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //игрок отошел от НПС
    {
        if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void Update()
    {
        GameObject MissionTagScanner = GameObject.FindGameObjectWithTag("Player"); // персонаж у которого берем квест будет взаимодействовать только с тем объектом у которого тэг Player;

        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // При нажатии на клавишу Е и если игрок рядом с НПС
        {
            vis = true;
            PinD.InDialoge = true;
        }
    }

    void DialogeState()
    {
        PC.movementSpeed = 0;
        PC.jumpForce = 0;
        PC.dashSpeed = 0;
        PCC.combatEnabled = false;
    }

    void DialogeExit()
    {
        PC.movementSpeed = 7;
        PC.jumpForce = 16;
        PC.dashSpeed = 20;
        PCC.combatEnabled = true;
    }

    void OnGUI()
    {
            if (trigger && vis == false)
            {
                GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 110, 25), "[Е] Поговорить");
            }
        
        if (vis)
        {
            MM.LastAction = "";
            DialogeState();
            GUI.Box(new Rect( //лицо ГГ ПЛЭЙЕРА
                    Screen.width - Screen.height / 4 - 15,
                    Screen.height * 3 / 4 - 15,
                    Screen.height / 4,
                    Screen.height / 4),
                    MM.PlayerIcon);


            GUI.Box(new Rect( //шкала отношений
                    15,
                    Screen.height / 4 + Screen.height / 10,
                    Screen.height / 4 + Screen.height / 8, //ширина
                    Screen.height / 8), //высота
                    "");

            GUI.Box(new Rect( //лицо перса нпс
                    15, 
                    Screen.height * 2 / 4 - 15, 
                    Screen.height * 2 / 4, 
                    Screen.height * 2 / 4), 
                    Emotions[0]);
                GUI.Box(new Rect( //плашка с именем нпс
                    30 + Screen.height * 2 / 4,
                    Screen.height * 3 / 4 - 15,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width/3),
                    Screen.height / 4),
                    CharName);
                GUI.Label(new Rect( // слова перса
                    40 + Screen.height/2, 
                    Screen.height * 3 / 4 + 5,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 3) - 10, 
                    250),
                    Phrases[0]);
                GUI.Label(new Rect( //близость
                    Screen.width - 100, 
                    Screen.height - 40, 
                    290, 
                    250), 
                    "Близость:" + intimacy);
                
                if (GUI.Button(new Rect( // ответ 1
                    Screen.width * 2 / 3,
                    Screen.height * 3 / 4 - 15,
                    Screen.width - 75 - Screen.height / 4 - (Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 3)) - Screen.height/2,
                    Screen.height/4/4),
                    "Прости, что побеспокоил")) 
                {
                    intimacy += 10;
                    vis = false;
                    PinD.InDialoge = false;
                    DialogeExit();
                }

                if (GUI.Button(new Rect( // ответ 2
                    Screen.width * 2 / 3,
                    Screen.height * 3 / 4 - 15 + Screen.width / 4 / 20 + Screen.height / 4 / 4,
                    Screen.width - 75 - Screen.height / 4 - (Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 3)) - Screen.height / 2,
                    Screen.height / 4 / 4), 
                    "Уже ухожу"))
                {
                    vis = false;
                    PinD.InDialoge = false;
                    DialogeExit();
                }

                if (GUI.Button(new Rect( // ответ 3
                    Screen.width * 2 / 3,
                    Screen.height * 3 / 4 - 15 + 2*Screen.width / 4 / 20 + 2*Screen.height / 4 / 4,
                    Screen.width - 75 - Screen.height / 4 - (Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 3)) - Screen.height / 2,
                    Screen.height / 4 / 4),
                    "Не твое дело"))
                {
                    intimacy -= 10;
                    vis = false;
                    PinD.InDialoge = false;
                    DialogeExit();
                }
        }
    }
}
   