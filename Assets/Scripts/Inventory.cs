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
    public Image[] Icon; // ����� � ���������
    public Sprite[] Sprites; //������ ��� �� �������� ���� ��������, ������� �� ������ � ������
    public Image[] CloseButtons;
    public int[] ItemCount;
    public TMP_Text[] ItemCountFields;

    private MissionManager MM;
    public List<string> InventoryObjects = new List<string>();

    public Object[] ObjectsInInventory;

    //public Texture2D CloseButton;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();

        for (int i = 0; i < InventoryObjects.Count; i++)
        {
            ItemCountFields[i].text = "";
        }
    }

    void Update()
    {
        for (int i = 0; i < InventoryObjects.Count; i++)
        {
            if (InventoryObjects[i] != "-")
            {
                ItemCountFields[i].text = ItemCount[i].ToString();
            }

            else
            {
                ItemCount[i] = 0;
                ItemCountFields[i].text = "";
            }
        }
    }

    void OnGUI()
    {
        for (int i = 0; i < InventoryObjects.Count; i++)
        {
            CloseButtons[i].sprite = Sprites[0];

            if (InventoryObjects[i] != "-")
            {
                CloseButtons[i].sprite = Sprites[1];
                
            }
        }
    }
}