using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Object
{
    public string Name;
    public int Amount;
    public Sprite Sprite;
    public int Attack;
    public int Durability;

    public Object(string name, int amount, Sprite sprite, int attack, int durability)
    {
        Name = name;
        Amount = amount;
        Sprite = sprite;
        Attack = attack;
        Durability = durability;
    }
}

public class Inventory : MonoBehaviour
{
    public Image[] Icon; // слоты в инвентаре
    public Sprite[] Sprites; //просто пул из спрайтов всех объектов, которые мы крепим к иконке
    public Image[] CloseButtons;
    public int[] ItemCount;
    public TMP_Text[] ItemCountFields;

    private MissionManager MM;
    public List<string> InventoryObjects = new List<string>();

    public Object[] ObjectsInInventory;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();

        for (int i = 0; i < ObjectsInInventory.Length; i++)
        {
            ItemCountFields[i].text = "";
            Icon[i].sprite = Sprites[4];
        }
    }

    void Update()
    {
        for (int i = 0; i < ObjectsInInventory.Length; i++)
        {
            if (ObjectsInInventory[i].Name != "")
            {
                ItemCountFields[i].text = ObjectsInInventory[i].Amount.ToString();
                Icon[i].sprite = ObjectsInInventory[i].Sprite;
            }

            else
            {
                ObjectsInInventory[i].Amount = 0;
                ObjectsInInventory[i].Attack = 0;
                ObjectsInInventory[i].Durability = 0;

                Icon[i].sprite = ObjectsInInventory[i].Sprite;
                ItemCountFields[i].text = "";
            }
        }
    }

    void OnGUI()
    {
        for (int i = 0; i < ObjectsInInventory.Length; i++)
        {
            CloseButtons[i].sprite = Sprites[0];

            if (ObjectsInInventory[i].Name != "")
            {
                CloseButtons[i].sprite = Sprites[1];
            }
        }
    }
}