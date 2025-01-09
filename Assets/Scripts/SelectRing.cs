using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRing : MonoBehaviour
{
    public Image[] images;
    public Sprite equipSprite;
    public Object EquipmentInSlot;

    public bool IsSelected;
    public Sprite SelectedSlot;
    public Sprite BasicSlot;
    public Instruments Inst;

    private Object equip;

    void Start()
    {
        Inst = GameObject.Find("Inventory").GetComponent<Instruments>();
        EquipmentInSlot.Name = "Hand";
    }

    void Update()
    {
        images = GetComponentsInChildren<Image>();

        equipSprite = images[1].sprite;
        equip = System.Array.Find(Inst.RingsInInventory, obj => obj.Sprite == equipSprite);
        if (equip != null)
        {
            EquipmentInSlot = equip;
        }

        if (Inst.SelectedRing != EquipmentInSlot)
        {
            gameObject.GetComponent<Image>().sprite = BasicSlot;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void SelectEquipment()
    {
        if (EquipmentInSlot.Name != "Hand")
        {
            Inst.SelectedRing = equip;
            IsSelected = true;

            gameObject.GetComponent<Image>().sprite = SelectedSlot;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(0.835f, 0.835f, 0.835f);
        }
    }
}
