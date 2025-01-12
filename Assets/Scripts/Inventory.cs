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
    public string Description;
    public string Type;
    public int Amount;
    public Sprite Sprite;
    public int Attack;
    public int AdditionalHealth;
    public int Durability;
    public float EnergyReduction;
    public float AddSpeed;
    public float AddJumpHeight;
    public int AddJumpsAmount;
    public Recipe[] ItemRecipe;
    public GameObject ItemPrefab;

    public Object(string name, string desc, string type, int amount, Sprite sprite, int attack, int health, int durability, float energyReduction, float addSpeed, float addJumpHeight, int addJumpsAmount, Recipe[] recipe, GameObject prefab)
    {
        Name = name;
        Description = desc;
        Type = type;
        Amount = amount;
        Sprite = sprite;
        Attack = attack;
        AdditionalHealth = health;
        Durability = durability;
        EnergyReduction = energyReduction;
        AddSpeed = addSpeed;
        AddJumpHeight = addJumpHeight;
        AddJumpsAmount = addJumpsAmount;
        ItemRecipe = recipe;
        ItemPrefab = prefab;
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
            if (ObjectsInInventory[i].Name != "") // если в инвентаре что-то есть
            {
                ItemCountFields[i].text = ObjectsInInventory[i].Amount.ToString();
                Icon[i].sprite = ObjectsInInventory[i].Sprite;
            }

            else
            {
                ObjectsInInventory[i].Type = "";
                ObjectsInInventory[i].Description = "";
                ObjectsInInventory[i].Amount = 0;
                ObjectsInInventory[i].Sprite = null;
                ObjectsInInventory[i].Attack = 0;
                ObjectsInInventory[i].AdditionalHealth = 0;
                ObjectsInInventory[i].Durability = 0;
                ObjectsInInventory[i].EnergyReduction = 0;
                ObjectsInInventory[i].AddSpeed = 0;
                ObjectsInInventory[i].AddJumpHeight = 0;
                ObjectsInInventory[i].AddJumpsAmount = 0;
                ObjectsInInventory[i].ItemRecipe = new Recipe[0];
                ObjectsInInventory[i].ItemPrefab = null;

                Icon[i].sprite = Sprites[4];
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