using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSelector : MonoBehaviour
{
    private TMP_Text SelectedNameShop;
    private TMP_Text SelectedInfo;
    private TMP_Text SelectedCost;

    public TMP_Text PriceInAss;
    public int ShopIndex;

    public Object ObjectBuy;
    public int CostBuy;

    public GameObject SlotSprite;
    private GameObject SelectedSlotSprite;

    private Shop SH;
    private Inventory Inv;
    private Instruments Inst;

    public void OnButtonClick()
    {
        SH.ItemForBuy = ObjectBuy;
        SH.ItemForBuyCost = CostBuy;

        SelectedNameShop.text = ObjectBuy.Name;
        SelectedCost.text = CostBuy.ToString();
        SelectedSlotSprite.GetComponent<Image>().sprite = ObjectBuy.Sprite;

        switch (ObjectBuy.Type)
        {
            case ("Sword"):
                SelectedInfo.text = $"Атака: {ObjectBuy.Attack}\nПрочность: {ObjectBuy.Durability}";
                break;
            case ("Molot"):
                SelectedInfo.text = $"Эффективность: {ObjectBuy.EnergyReduction}\nПрочность: {ObjectBuy.Durability}";
                break;
            case ("Chest"):
                SelectedInfo.text = $"Здоровье: {ObjectBuy.AdditionalHealth}\nПрочность: {ObjectBuy.Durability}";
                break;
            case ("Ring"):
                SelectedInfo.text = $"Скорость: {ObjectBuy.AddSpeed}\nВысота прыжков: {ObjectBuy.AddJumpHeight}\nКоличество прыжков: {ObjectBuy.AddJumpsAmount}\nПрочность: {ObjectBuy.Durability}";
                break;
            case ("Material"):
                SelectedInfo.text = $"{ObjectBuy.Description}";
                break;
        }
    }

    void Start()
    {
        SelectedSlotSprite = GameObject.Find("SelectedShopObjectSprite");

        SelectedNameShop = GameObject.Find("SelectedNameShop").GetComponent<TMP_Text>();
        SelectedInfo = GameObject.Find("ShopItemInfo").GetComponent<TMP_Text>();
        SelectedCost = GameObject.Find("PriceSelectedText").GetComponent<TMP_Text>();
        SH = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<Shop>();
        Inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        Inst = GameObject.Find("Inventory").GetComponent<Instruments>();
    }

    void Update()
    {
        ObjectBuy = SH.ItemsInShop[ShopIndex].ItemForBuy.GetComponent<ObjectWeapon>().Item;
        CostBuy = SH.ItemsInShop[ShopIndex].Cost;
        SlotSprite.GetComponent<Image>().sprite = ObjectBuy.Sprite;

        PriceInAss.text = CostBuy.ToString();
    }
}
