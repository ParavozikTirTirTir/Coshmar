using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Instrument
{
    public string Name;
    public string Type;
    public int Amount;
    public Sprite Sprite;
    public int Attack;
    public int Durability;
    public double EnergyReduction;

    public Instrument(string name, string type, int amount, Sprite sprite, int attack, int durability, double energyReduction)
    {
        Name = name;
        Type = type;
        Amount = amount;
        Sprite = sprite;
        Attack = attack;
        Durability = durability;
        EnergyReduction = energyReduction;
    }
}

public class Instruments : MonoBehaviour
{
    public GameObject SelectedSword;
    public GameObject SelectedMolot;

    public Image[] SwordPlace;
    public Image[] MolotPlace;

    public TMP_Text[] ItemCountFields;
    public Image[] Icon; // все слоты в инвентаре

    private MissionManager MM;

    public Instrument[] SwordsInInventory;
    public Instrument[] MolotsInInventory;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();

        //for (int i = 0; i < InstrumentsInInventory.Length; i++)
        //{
        //    ItemCountFields[i].text = "";
        //}
    }

    void Update()
    {
        //for (int i = 0; i < InstrumentsInInventory.Length; i++)
        //{
        //    if (InstrumentsInInventory[i].Name != "")
        //    {
        //        ItemCountFields[i].text = InstrumentsInInventory[i].Amount.ToString();
        //        Icon[i].sprite = InstrumentsInInventory[i].Sprite;
        //    }

        //    else
        //    {
        //        InstrumentsInInventory[i].Amount = 0;
        //        InstrumentsInInventory[i].Attack = 0;
        //        InstrumentsInInventory[i].Durability = 0;

        //        Icon[i].sprite = InstrumentsInInventory[i].Sprite;
        //        ItemCountFields[i].text = "";
        //    }
        //}
    }
}
