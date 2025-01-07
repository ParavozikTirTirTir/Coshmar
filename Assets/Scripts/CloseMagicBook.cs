using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.UIElements;
using System;

public class CloseMagicBook : MonoBehaviour, IPointerDownHandler
{
    public Canvas MagicBook;
    private OpenInventory OI;
    private OpenMagicBook MB;

    void Start()
    {
        OI = GameObject.FindGameObjectWithTag("InvCanvas").GetComponent<OpenInventory>();
        MB = GameObject.Find("MagicBook").GetComponent<OpenMagicBook>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MagicBook.enabled = false;
        MB.OpenBookCheck = false;
        OI.OpenInventoryCheck = !OI.OpenInventoryCheck;
        OI.canvas.enabled = !OI.canvas.enabled;
    }
}
