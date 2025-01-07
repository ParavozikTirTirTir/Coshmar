using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class ChestTorch : MonoBehaviour
{
    private bool trigger = false;
    public bool CanGiveAnItem; //сундук дает предмет или нет

    private bool MissionDone = false; //переменная, которая определеяет, что квест уже сделан

    public string RewardName; //награда из сундука если она есть
    public Sprite RewardSprite; //спрайт награды из сундука
    public Sprite OpenedChest; //спрайт открытого сундука

    private MissionManager MM;
    private Inventory Inv;
    private int ObjectIndexInInventory;
    private int EmptyIndexInInventory;
    public int MoneyForMission;

    public int NumberTorchesOn;

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

        // открытие сундука
        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !MissionDone && NumberTorchesOn == 3)
        {
            MM.LastAction = "Открыт сундук";
            GetComponent<SpriteRenderer>().sprite = OpenedChest;
            MissionDone = true;

            MM.Money = MM.Money + MoneyForMission; //добавление денег за выполнение квеста;

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
        if (trigger && !MissionDone && NumberTorchesOn == 3)
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 110, 25), "[Е] Открыть");
        }
    }
}
   