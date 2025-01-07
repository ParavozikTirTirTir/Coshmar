using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class MissionBot : MonoBehaviour
{
    public bool trigger = false;
    public bool vis;
    public bool CanGiveAnItem;
    public bool MissionDone = false; //переменная, которая определеяет, что квест уже сделан
    public string NpcName;
    public string MissionName; // Текст который будет отображать наименование квеста
    public string MissionDialoge; // Текст диалога в квесте
    public string MissionDialogeDone; // Текст диалога в квесте
    public string MissionObjectName; //Квестовый предмет
    public string MissionPriority = "1";
    public string MissionInformation; //Информация о миссии, которая выводится при нажатии на миссию в левом верхнем углу

    public string RewardName; //Квестовый предмет
    public Sprite RewardSprite;
    public Texture2D[] Emotions;

    private MissionManager MM;
    private Inventory Inv;
    private PlayerController PC;
    private PlayerCombatController PCC;
    private IsPlayerInDialoge PinD;
    private int ObjectIndexInInventory;
    private int EmptyIndexInInventory;
    public int MoneyForMission;

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();
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

        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !MissionDone) // При нажатии на клавишу Е и если игрок рядом с НПС
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
        MM.LastAction = "";
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
        if (vis)
        {
            DialogeState();

            GUI.Box(new Rect( //лицо перса
                    15,
                    Screen.height * 2 / 4 - 15,
                    Screen.height * 2 / 4,
                    Screen.height * 2 / 4),
                    Emotions[0]);

            if (!MM.MissionsInProgress.Contains(MissionName) && !MissionDone) //если квест еще не взят и не выполнен;
            {
                GUI.Box(new Rect( //плашка с именем
                    30 + Screen.height * 2 / 4,
                    Screen.height * 3 / 4 - 15,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 4),
                    Screen.height / 4),
                    NpcName); //на экране отображается окно с названием Квеста;
                GUI.Label(new Rect( // слова перса
                    40 + Screen.height * 2 / 4,
                    Screen.height * 3 / 4 + 20,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 4) - 10,
                    250),
                    MissionDialoge); //текстом описывает квест;
                if (GUI.Button(new Rect( // ответ 1
                    Screen.width * 3 / 4,
                    Screen.height * 3 / 4 - 15,
                    200,
                    Screen.height / 4 / 4),
                    "Принять задание")) // при нажатии на кнопку Ok;
                {
                    MM.MissionsInProgress.Add(MissionName);
                    MM.MissionsPriority.Add(MissionPriority);
                    MM.MissionsInformation.Add(MissionInformation);
                    MM.MissionsObjectName.Add(MissionObjectName);
                    vis = false; // все диалоговые окна закрываются;
                    PinD.InDialoge = false;

                    MM.LastAction = "Принят квест [" + MissionName + "]";
                    DialogeExit();
                }

                if (GUI.Button(new Rect( // ответ 2
                    Screen.width * 3 / 4,
                    Screen.height * 3 / 4 - 15 + Screen.width / 4 / 20 + Screen.height / 4 / 4,
                    200,
                    Screen.height / 4 / 4),
                    "Не сейчас"))
                {
                    vis = false;
                    PinD.InDialoge = false;
                    DialogeExit();
                }
            }

            if (MM.MissionsInProgress.Contains(MissionName) && !MissionDone) // если квест уже взят, но не завершен;
            {
                GUI.Box(new Rect( //плашка с именем
                    30 + Screen.height * 2 / 4,
                    Screen.height * 3 / 4 - 15,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 4),
                    Screen.height / 4),
                    NpcName);
                GUI.Label(new Rect( // слова перса
                    40 + Screen.height * 2 / 4,
                    Screen.height * 3 / 4 + 20,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 4) - 10,
                    250),
                    MissionDialogeDone); //то описание квеста меняется на другой текст;
                if (Inv.InventoryObjects.Contains(MissionObjectName))
                {
                    if (GUI.Button(new Rect( // ответ 1
                    Screen.width * 3 / 4,
                    Screen.height * 3 / 4 - 15,
                    200,
                    Screen.height / 4 / 4),
                    "Отдать предмет")) // то появится кнопка да, при нажатии на которую;
                    {
                        ObjectIndexInInventory = Inv.InventoryObjects.IndexOf(MissionObjectName);
                        if (Inv.ItemCount[ObjectIndexInInventory] > 1)
                        {
                            Inv.ItemCount[ObjectIndexInInventory] -= 1;
                        }

                        if (Inv.ItemCount[ObjectIndexInInventory] == 1)
                        {
                            Inv.ItemCount[ObjectIndexInInventory] -= 1;
                            Inv.Icon[ObjectIndexInInventory].sprite = Inv.Sprites[4];
                            Inv.InventoryObjects.Insert(ObjectIndexInInventory, "-");
                        }
                        //Inv.Icon[ObjectIndexInInventory].sprite = Inv.Sprites[4];
                        //Inv.InventoryObjects.Insert(ObjectIndexInInventory, "-");
                        //Inv.InventoryObjects.Remove(MissionObjectName);

                        MM.LastAction = "Закончен квест [" + MissionName + "]";
                        MM.MissionsInProgress.Remove(MissionName); // убираем квест из списка активных
                        MM.MissionsPriority.Remove(MissionPriority);
                        MM.MissionsInformation.Remove(MissionInformation);
                        MM.MissionsObjectName.Remove(MissionObjectName);

                        if (CanGiveAnItem)
                        {
                            if (Inv.InventoryObjects.Contains(RewardName))
                            {
                                Inv.ItemCount[Inv.InventoryObjects.IndexOf(RewardName)] += 1;
                            }

                            else
                            {
                                EmptyIndexInInventory = Inv.InventoryObjects.IndexOf("-");
                                Inv.ItemCount[EmptyIndexInInventory] += 1;
                                Inv.Icon[EmptyIndexInInventory].sprite = RewardSprite;
                                Inv.InventoryObjects.Insert(EmptyIndexInInventory, RewardName);
                                Inv.InventoryObjects.Remove("-");
                            }

                            MM.LastAction = "Закончен квест [" + MissionName + "] и получен предмет [" + RewardName + "]";
                        }

                        MM.Money = MM.Money + MoneyForMission; //добавление денег за выполнение квеста;
                        vis = false; // диалоговое окно закрывается;
                        PinD.InDialoge = false;
                        MissionDone = true;

                        DialogeExit();
                    }
                }

                else
                { // если вы еще не подобрали объект;
                    if (GUI.Button(new Rect( // ответ 1
                    Screen.width * 3 / 4,
                    Screen.height * 3 / 4 - 15,
                    200,
                    Screen.height / 4 / 4),
                    "Я зайду позже")) // то вместо кнопки да, будет кнопка нет;
                    {
                        vis = false; // при нажатии на которую, окно просто закроется;
                        PinD.InDialoge = false;

                        DialogeExit();
                    }
                }
            }
        }
    }
}
   