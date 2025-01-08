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
    public string ObjectName;
    public string Stats;
    public int Attack;
    public int Durability;
    private Inventory Inv;
    private SpriteRenderer ThisObjectSprite;
    public int EmptyIndexInInventory;
    public GameObject ObjectItem;
    public Recipe[] recipes;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        ThisObjectSprite = gameObject.GetComponent<SpriteRenderer>();
        ObjectItem = gameObject;
    }

    void OnTriggerStay2D(Collider2D obj) //������ �� ������
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //���� ����� �� ������
    {
        if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // ���� ����� ����� � �������� � ����� �
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

            for (int i = 0; i < Inv.ObjectsInInventory.Length; i++)
            {
                if (Inv.ObjectsInInventory[i].Amount == 0)
                {
                    Inv.ObjectsInInventory[i].Name = ObjectName;
                    Inv.ObjectsInInventory[i].Sprite = ThisObjectSprite.sprite;
                    Inv.ObjectsInInventory[i].Attack = Attack;
                    Inv.ObjectsInInventory[i].Durability = Durability;
                    Inv.ObjectsInInventory[i].Amount = 1;
                    break;
                }

                if (Inv.ObjectsInInventory[i].Name == ObjectName)
                {
                    Inv.ObjectsInInventory[i].Amount += 1;
                    break;
                }
            }

            MM.LastAction = "�������� [" + ObjectName + "]";

            Destroy(gameObject); // � ������� ���� ������ �� �����;
        }
    }

    void OnGUI() //������ �������
    {
        if (trigger) //����� �������� �� ������
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 90, 25), "[E] �������");
        }
    }
}
