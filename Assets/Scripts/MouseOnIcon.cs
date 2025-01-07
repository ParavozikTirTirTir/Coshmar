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
            Inv.Icon[ButtonIndex].sprite = Inv.Sprites[4];
            MM.LastAction = "Выброшен предмет [" + Inv.InventoryObjects[ButtonIndex] + "]";
            Inv.InventoryObjects[ButtonIndex] = "-";
            Debug.Log("нажалась кнопка я хз");
        }
    }

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
    }

    void Update()
    {

        if (Inv.CloseButtons[ButtonIndex].sprite == Inv.Sprites[0]) //если кнопка невидимая
        {
            IsCloseButtonVisible = false;
        }

        if (Inv.CloseButtons[ButtonIndex].sprite == Inv.Sprites[1]) //если кнопка видимая
        {
            IsCloseButtonVisible = true;
        }
    }
}
