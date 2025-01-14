using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

[System.Serializable]

public class BuyObject
{
    public GameObject ItemForBuy;
    public int Cost;

    public BuyObject(GameObject item, int cost)
    {
        ItemForBuy = item;
        Cost = cost;
    }
}

public class Shop : MonoBehaviour
{
    public BuyObject[] ItemsInShop;
    public Object ItemForBuy;
    public int ItemForBuyCost;

    private Inventory Inv;
    private Instruments Inst;

    public TMP_Text RecipeText;
    private TMP_Text ToolInfoString;
    private TMP_Text ToolInfo;

    public Sprite PNGpicture;


    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        Inst = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Instruments>();
    }

    void Update()
    {
        
    }

    public void BuyButtonClick()
    {
        if (ItemForBuy.Name != "" && ItemForBuyCost<=Inst.Coins)
        {
            Inst.Coins -= ItemForBuyCost;

            bool itemFound = false;

            for (int i = 0; i < Inv.ObjectsInInventory.Length; i++)
            {
                if (Inv.ObjectsInInventory[i].Name == ItemForBuy.Name)
                {
                    Inv.ObjectsInInventory[i].Amount += ItemForBuy.Amount;
                    itemFound = true;
                    break;
                }
            }

            if (!itemFound)
            {
                for (int i = 0; i < Inv.ObjectsInInventory.Length; i++)
                {
                    if (Inv.ObjectsInInventory[i].Amount == 0)
                    {
                        Inv.ObjectsInInventory[i] = ItemForBuy.Clone();
                        break;
                    }
                }
            }
        }
    }
}
