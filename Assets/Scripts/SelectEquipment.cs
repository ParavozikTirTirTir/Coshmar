using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectEquipment : MonoBehaviour
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

    void Start()
    {
        InstrumentType = gameObject.tag;

        Inst = GameObject.Find("Inventory").GetComponent<Instruments>();

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
        if (EquipmentInSlot.Name != "")
        {
            switch (InstrumentType)
            {
                case "InvSword":
                    Inst.SelectedSword = equip;
                    break;
                case "InvMolot":
                    Inst.SelectedMolot = equip;
                    break;
                case "InvChest":
                    Inst.SelectedChest = equip;
                    break;
                case "InvRing":
                    Inst.SelectedRing = equip;
                    break;
            }

            IsSelected = true;

            gameObject.GetComponent<Image>().sprite = SelectedSlot;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(0.835f, 0.835f, 0.835f);
        }
    }
}
