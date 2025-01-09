using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectEquipment : MonoBehaviour
{
    public Image[] images;
    public Sprite swordSprite;
    public Object SwordInSlot;

    public bool IsSelected;
    public Sprite SelectedSlot;
    public Sprite BasicSlot;
    public Instruments Inst;

    private Object sword;

    void Start()
    {
        Inst = GameObject.Find("Inventory").GetComponent<Instruments>();
        SwordInSlot.Name = "Hand";
    }

    void Update()
    {
        images = GetComponentsInChildren<Image>();

        swordSprite = images[1].sprite;
        sword = System.Array.Find(Inst.SwordsInInventory, obj => obj.Sprite == swordSprite);
        if (sword != null)
        {
            SwordInSlot = sword;
        }

        if (Inst.SelectedSword != SwordInSlot)
        {
            gameObject.GetComponent<Image>().sprite = BasicSlot;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void SelectSword()
    {
        if (SwordInSlot.Name != "Hand")
        {
            Inst.SelectedSword = sword;
            IsSelected = true;

            gameObject.GetComponent<Image>().sprite = SelectedSlot;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(0.835f, 0.835f, 0.835f);
        }
    }
}
