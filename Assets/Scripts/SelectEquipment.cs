using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectEquipment : MonoBehaviour, IPointerEnterHandler//, IPointerExitHandler
{
    public string InstrumentType; //InvSword, InvMolot, InvChest, InvRing

    public Image[] images;
    public Sprite equipSprite;
    public Object EquipmentInSlot;

    public bool IsSelected;
    public Sprite SelectedSlot;
    public Sprite BasicSlot;
    public Instruments Inst;

    private Object equip;
    public Sprite pngSprite;
    private Object NoEquipInSlot;

    private Object SelectedEquip;

    private Image InfoField;
    private TMP_Text NamePlace;
    private TMP_Text StatsNamesPlace;
    private TMP_Text StatsPlace;

    public Object EmptyObject;

    void Start()
    {
        InstrumentType = gameObject.tag;

        Inst = GameObject.Find("Inventory").GetComponent<Instruments>();

        InfoField = GameObject.Find("InfoAboutEquip").GetComponent<Image>();
        InfoField.enabled = false;
        NamePlace = GameObject.Find("EquipNameInfo").GetComponent<TMP_Text>();
        StatsNamesPlace = GameObject.Find("EquipStatsName").GetComponent<TMP_Text>();
        StatsPlace = GameObject.Find("EquipStatsInfo").GetComponent<TMP_Text>();
        NamePlace.text = "";
        StatsNamesPlace.text = "";
        StatsPlace.text = "";

        images = GetComponentsInChildren<Image>();
        pngSprite = images[1].sprite;

        NoEquipInSlot = EquipmentInSlot;
    }

    void Update()
    {
        images = GetComponentsInChildren<Image>();
        equipSprite = images[1].sprite;

        switch (InstrumentType)
        {
            case "InvSword":
                equip = System.Array.Find(Inst.SwordsInInventory, obj => obj.Sprite == equipSprite);
                SelectedEquip = Inst.SelectedSword;
                break;
            case "InvMolot":
                equip = System.Array.Find(Inst.MolotsInInventory, obj => obj.Sprite == equipSprite);
                SelectedEquip = Inst.SelectedMolot;
                break;
            case "InvChest":
                equip = System.Array.Find(Inst.ChestsInInventory, obj => obj.Sprite == equipSprite);
                SelectedEquip = Inst.SelectedChest;
                break;
            case "InvRing":
                equip = System.Array.Find(Inst.RingsInInventory, obj => obj.Sprite == equipSprite);
                SelectedEquip = Inst.SelectedRing;
                break;
        }

        if (equip != null)
        {
            EquipmentInSlot = equip;
        }

        if (SelectedEquip != EquipmentInSlot || equip == null)
        {
            gameObject.GetComponent<Image>().sprite = BasicSlot;
            IsSelected = false;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(1f, 1f, 1f);
        }

        if (EquipmentInSlot == SelectedEquip)
        {
            IsSelected = true;

            gameObject.GetComponent<Image>().sprite = SelectedSlot;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(0.835f, 0.835f, 0.835f);
        }

        if (equipSprite == pngSprite)
        {
            EquipmentInSlot = NoEquipInSlot;
        }
    }

    public void SelectEquipmentClick()
    {
        if (EquipmentInSlot.Name != "" && !IsSelected)
        {
            switch (InstrumentType)
            {
                case "InvSword":
                    var equipSword = System.Array.Find(Inst.SwordsInInventory, obj => obj.Sprite == equipSprite);
                    Inst.SelectedSword = equipSword;
                    break;
                case "InvMolot":
                    var equipMolot = System.Array.Find(Inst.MolotsInInventory, obj => obj.Sprite == equipSprite);
                    Inst.SelectedMolot = equipMolot;
                    break;
                case "InvChest":
                    var equipChest = System.Array.Find(Inst.MolotsInInventory, obj => obj.Sprite == equipSprite);
                    Inst.SelectedChest = equipChest;
                    break;
                case "InvRing":
                    var equipRing = System.Array.Find(Inst.MolotsInInventory, obj => obj.Sprite == equipSprite);
                    Inst.SelectedRing = equipRing;
                    break;
            }

            IsSelected = true;

            gameObject.GetComponent<Image>().sprite = SelectedSlot;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(0.835f, 0.835f, 0.835f);
        }

        else
        {
            switch (InstrumentType)
            {
                case "InvSword":
                    Inst.SelectedSword = EmptyObject;
                    break;
                case "InvMolot":
                    Inst.SelectedMolot = EmptyObject;
                    break;
                case "InvChest":
                    Inst.SelectedChest = EmptyObject;
                    break;
                case "InvRing":
                    Inst.SelectedRing = EmptyObject;
                    break;
            }

            IsSelected = false;

            gameObject.GetComponent<Image>().sprite = SelectedSlot;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(EquipmentInSlot.Name != "")
        {
            InfoField.enabled = true;
            NamePlace.text = EquipmentInSlot.Name;
            switch (EquipmentInSlot.Type)
            {
                case "Sword":
                    StatsNamesPlace.text = $"Сила атаки\nПрочность";
                    StatsPlace.text = $"{EquipmentInSlot.Attack}\n{EquipmentInSlot.Durability}";
                    break;
                case "Molot":
                    StatsNamesPlace.text = $"Эффективность\nПрочность";
                    StatsPlace.text = $"{EquipmentInSlot.EnergyReduction}%\n{EquipmentInSlot.Durability}";
                    break;
                case "Chest":
                    StatsNamesPlace.text = $"Здоровье\nПрочность";
                    StatsPlace.text = $"{EquipmentInSlot.AdditionalHealth}\n{EquipmentInSlot.Durability}";
                    break;
                case "Ring":
                    StatsNamesPlace.text = $"Скорость\nВысота прыжков\nКоличество прыжков";
                    StatsPlace.text = $"{EquipmentInSlot.AddSpeed}\n{EquipmentInSlot.AddJumpHeight}\n{EquipmentInSlot.AddJumpsAmount}";
                    break;
            }
        }
    }

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    InfoField.enabled = false;
    //}
}
