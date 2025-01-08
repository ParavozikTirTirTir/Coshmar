using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.UIElements;
using System;

public class MouseOnIcon : MonoBehaviour, IPointerDownHandler
{
    public bool IsCloseButtonVisible;
    private Inventory Inv;
    private MissionManager MM;
    public int ButtonIndex;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsCloseButtonVisible)
        {
            Inv.ObjectsInInventory[ButtonIndex].Name = "";
            Inv.ObjectsInInventory[ButtonIndex].Amount = 0;
            Inv.ObjectsInInventory[ButtonIndex].Sprite = Inv.Sprites[4];
            Inv.ObjectsInInventory[ButtonIndex].Attack = 0;
            Inv.ObjectsInInventory[ButtonIndex].Durability = 0;

            Inv.Icon[ButtonIndex].sprite = Inv.Sprites[4];
            MM.LastAction = "�������� ������� [" + Inv.ObjectsInInventory[ButtonIndex].Name + "]";
        }
    }

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
    }

    void Update()
    {
        if (Inv.CloseButtons[ButtonIndex].sprite == Inv.Sprites[0]) //���� ������ ���������
        {
            IsCloseButtonVisible = false;
        }

        if (Inv.CloseButtons[ButtonIndex].sprite == Inv.Sprites[1]) //���� ������ �������
        {
            IsCloseButtonVisible = true;
        }
    }
}
