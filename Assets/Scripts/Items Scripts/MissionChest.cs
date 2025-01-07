using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class MissionChest : MonoBehaviour
{
    private bool trigger = false;
    public bool CanGiveAnItem; //сундук дает предмет или нет

    public bool MissionDone = false; //переменная, которая определеяет, что квест уже сделан
    public string MissionName; // Текст который будет отображать наименование квеста
    public string MissionObjectName; //Квестовый предмет
    public string MissionPriority = "2";
    public string MissionInformation;

    public string RewardName; //награда из сундука если она есть
    public Sprite RewardSprite; //спрайт награды из сундука
    public Sprite OpenedChest; //спрайт открытого сундука

    private MissionManager MM;
    private Inventory Inv;
    private int ObjectIndexInInventory;
    private int EmptyIndexInInventory;
    public int MoneyForMission;

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
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
        GameObject MissionTagScanner = GameObject.FindGameObjectWithTag("Player");

        if (!MM.MissionsInProgress.Contains(MissionName) && !MissionDone && trigger) //если квест еще не взят и не выполнен;
        {
            MM.MissionsInProgress.Add(MissionName);
            MM.MissionsPriority.Add(MissionPriority);
            MM.MissionsInformation.Add(MissionInformation);
            MM.MissionsObjectName.Add(MissionObjectName);
            MM.LastAction = "Принят квест [" + MissionName + "]";
        }

        // открытие сундука
        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !MissionDone && MM.MissionsInProgress.Contains(MissionName) && Inv.InventoryObjects.Contains(MissionObjectName))
        {
            ObjectIndexInInventory = Inv.InventoryObjects.IndexOf(MissionObjectName); //УДАЛЕНИЕ КЛЮЧА ИЗ ИНВЕНТАРЯ
            Inv.Icon[ObjectIndexInInventory].sprite = Inv.Sprites[4];
            Inv.InventoryObjects.Insert(ObjectIndexInInventory, "-");
            Inv.InventoryObjects.Remove(MissionObjectName);

            MM.LastAction = "Открыт сундук";
            MM.MissionsInProgress.Remove(MissionName); //УДАЛЕНИЕ ИНФОРМАЦИИ О МИССИИ
            MM.MissionsPriority.Remove(MissionPriority);
            MM.MissionsInformation.Remove(MissionInformation);
            MM.MissionsObjectName.Remove(MissionObjectName);

            GetComponent<SpriteRenderer>().sprite = OpenedChest;

            MM.Money = MM.Money + MoneyForMission; //добавление денег за выполнение квеста;
            MissionDone = true;

            if (CanGiveAnItem) //добавление награды из сундука в инвентарь
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
            }
        }
    }

    void OnGUI() //кнопка открыть
    {
        if (trigger && !MissionDone && Inv.InventoryObjects.Contains(MissionObjectName))
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 110, 25), "[Е] Открыть");
        }
    }
}
   