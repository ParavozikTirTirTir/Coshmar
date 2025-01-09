using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[System.Serializable]
public class Recipe
{
    public string ComponentName;
    public int Amount;

    public Recipe(string name, int amount)
    {
        ComponentName = name;
        Amount = amount;
    }
}

public class ObjectWeapon : MonoBehaviour
{
    private MissionManager MM;
    public bool trigger = false;
    public string Type;
    public string ObjectName;
    public string Stats;
    public int Attack;
    public int Durability;
    public double EnergyReduction;

    private Inventory Inv;
    public SpriteRenderer ThisObjectSprite;
    public int EmptyIndexInInventory;
    public GameObject ObjectItem;
    public Recipe[] recipes;

    private OpenInventory OI;
    private OpenMagicBook MB;
    private OpenCraft OC;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MB = GameObject.Find("MagicBook").GetComponent<OpenMagicBook>();
        OI = GameObject.Find("InventoryCanvas").GetComponent<OpenInventory>();
        OC = GameObject.Find("Craft").GetComponent<OpenCraft>();
        ThisObjectSprite = gameObject.GetComponent<SpriteRenderer>();
        ObjectItem = gameObject;
    }

    void OnTriggerStay2D(Collider2D obj) //«Наезд» на объект
    {
        if (obj.tag == "Player" && !MB.OpenBookCheck && !OI.OpenInventoryCheck && !OC.OpenCraftCheck)
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // если игрок рядом с объектом и нажал Е
        {
            for (int i = 0; i < Inv.ObjectsInInventory.Length; i++)
            {
                if (Inv.ObjectsInInventory[i].Amount == 0)
                {
                    Inv.ObjectsInInventory[i].Name = ObjectName;
                    Inv.ObjectsInInventory[i].Type = Type;
                    Inv.ObjectsInInventory[i].Sprite = ThisObjectSprite.sprite;
                    Inv.ObjectsInInventory[i].Attack = Attack;
                    Inv.ObjectsInInventory[i].Durability = Durability;
                    Inv.ObjectsInInventory[i].EnergyReduction = EnergyReduction;
                    Inv.ObjectsInInventory[i].Amount = 1;
                    break;
                }

                if (Inv.ObjectsInInventory[i].Name == ObjectName)
                {
                    Inv.ObjectsInInventory[i].Amount += 1;
                    break;
                }
            }

            MM.LastAction = "Получено [" + ObjectName + "]";

            Destroy(gameObject); // и удаляем этот объект со сцены;
        }
    }

    void OnGUI() //кнопка собрать
    {
        if (trigger && !MB.OpenBookCheck && !OI.OpenInventoryCheck && !OC.OpenCraftCheck) //игрок наступил на объект
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 90, 25), "[E] Собрать");
        }
    }
}
