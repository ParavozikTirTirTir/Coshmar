using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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
    public Sprite EmptySlotSprite;
    public Object SelectedSword;
    public Object SelectedMolot;
    public Object SelectedChest;
    public Object SelectedRing;

    public Image SwordPlace;
    public Image MolotPlace;
    public Image ChestPlace;
    public Image RingPlace;

    public TMP_Text[] ItemCountFields;
    public Image[] IconSwords; // все слоты в инвентаре
    public Image[] IconMolots;
    public Image[] IconChests;
    public Image[] IconRings;

    private MissionManager MM;
    private Inventory Inv;

    public Object[] SwordsInInventory;
    public Object[] MolotsInInventory;
    public Object[] ChestsInInventory;
    public Object[] RingsInInventory;

    private Sprite SwordPlaceSprite;
    private Sprite MolotPlaceSprite;
    private Sprite ChestPlaceSprite;
    private Sprite RingPlaceSprite;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        Inv = GetComponent<Inventory>();

        SwordPlaceSprite = SwordPlace.sprite;
        MolotPlaceSprite = MolotPlace.sprite;
        ChestPlaceSprite = ChestPlace.sprite;
        RingPlaceSprite = RingPlace.sprite;

        //for (int i = 0; i < InstrumentsInInventory.Length; i++)
        //{
        //    ItemCountFields[i].text = "";
        //}
    }

    void Update()
    {
        if (SelectedSword.Name != "")
        {
            SwordPlace.sprite = SelectedSword.Sprite;
        }
        else
        {
            SwordPlace.sprite = SwordPlaceSprite;
        }

        if (SelectedMolot.Name != "")
        {
            MolotPlace.sprite = SelectedMolot.Sprite;
        }
        else
        {
            MolotPlace.sprite = MolotPlaceSprite;
        }

        if (SelectedChest.Name != "")
        {
            ChestPlace.sprite = SelectedChest.Sprite;
        }
        else
        {
            ChestPlace.sprite = ChestPlaceSprite;
        }

        if (SelectedRing.Name != "")
        {
            RingPlace.sprite = SelectedRing.Sprite;
        }
        else
        {
            RingPlace.sprite = RingPlaceSprite;
        }


        var swords = Inv.ObjectsInInventory.Where(obj => obj.Type == "Sword").ToArray();
        SwordsInInventory = swords;
        for (int i = 0; i < IconSwords.Length; i++)
        {
            if (i < SwordsInInventory.Length)
            {
                IconSwords[i].sprite = SwordsInInventory[i].Sprite;
            }
            else
            {
                IconSwords[i].sprite = EmptySlotSprite;
            }
        }

        var molots = Inv.ObjectsInInventory.Where(obj => obj.Type == "Molot").ToArray();
        MolotsInInventory = molots;
        for (int i = 0; i < IconMolots.Length; i++)
        {
            if (i < MolotsInInventory.Length)
            {
                IconMolots[i].sprite = MolotsInInventory[i].Sprite;
            }
            else
            {
                IconMolots[i].sprite = EmptySlotSprite;
            }
        }

        var chests = Inv.ObjectsInInventory.Where(obj => obj.Type == "Chest").ToArray();
        ChestsInInventory = chests;
        for (int i = 0; i < IconChests.Length; i++)
        {
            if (i < ChestsInInventory.Length)
            {
                IconChests[i].sprite = ChestsInInventory[i].Sprite;
            }
            else
            {
                IconChests[i].sprite = EmptySlotSprite;
            }
        }

        var rings = Inv.ObjectsInInventory.Where(obj => obj.Type == "Ring").ToArray();
        RingsInInventory = rings;
        for (int i = 0; i < IconRings.Length; i++)
        {
            if (i < RingsInInventory.Length)
            {
                IconRings[i].sprite = RingsInInventory[i].Sprite;
            }
            else
            {
                IconRings[i].sprite = EmptySlotSprite;
            }
        }
    }
}
