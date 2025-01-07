using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.UIElements;
using System;

public class OpenMagicBook : MonoBehaviour, IPointerDownHandler
{
    public Canvas MagicBook;
    private OpenInventory OI;
    private PlayerController PC;
    private PlayerCombatController PCC;
    public bool OpenBookCheck = false;

    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        MagicBook.enabled = false;
        OI = GameObject.FindGameObjectWithTag("InvCanvas").GetComponent<OpenInventory>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MagicBook.enabled = true;
        OpenBookCheck = true;

        OI.OpenInventoryCheck = !OI.OpenInventoryCheck;
        OI.canvas.enabled = !OI.canvas.enabled;
    }
}
