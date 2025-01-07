using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string ObjectName;
    public string Stats;
    public int Attack;
    public int Durability;
    private Inventory Inv;
    private SpriteRenderer ThisObjectSprite;
    public int EmptyIndexInInventory;
    public Recipe[] recipes;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        ThisObjectSprite = gameObject.GetComponent<SpriteRenderer>();
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // если игрок рядом с объектом и нажал Е
        {
            //MP.IsObjectCollected = true;
            //Inv.Icon[Inv.i].sprite = ThisObjectSprite.sprite;
            //Inv.InventoryObjects.Add(ObjectName);

            if (Inv.InventoryObjects.Contains(ObjectName))
            {
                Inv.ItemCount[Inv.InventoryObjects.IndexOf(ObjectName)] += 1;
            }

            else
            {
                EmptyIndexInInventory = Inv.InventoryObjects.IndexOf("-");
                Inv.ItemCount[EmptyIndexInInventory] += 1;
                Inv.Icon[EmptyIndexInInventory].sprite = ThisObjectSprite.sprite;
                Inv.InventoryObjects.Insert(EmptyIndexInInventory, ObjectName);
                Inv.InventoryObjects.Remove("-");
            }

            MM.LastAction = "Получено [" + ObjectName + "]";

            Destroy(gameObject); // и удаляем этот объект со сцены;
        }
    }

    void OnGUI() //кнопка собрать
    {
        if (trigger) //игрок наступил на объект
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 90, 25), "[E] Собрать");
        }
    }
}
