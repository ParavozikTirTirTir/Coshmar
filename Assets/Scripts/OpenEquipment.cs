using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEquipment : MonoBehaviour
{
    public Canvas Equipment;
    private OpenInventory OI;
    public bool OpenEquipmentCheck;
    private GameObject InvSwordsWindow;
    private GameObject InvMolotsWindow;
    private GameObject InvChestsWindow;
    private GameObject InvRingsWindow;

    void Start()
    {
        InvSwordsWindow = GameObject.Find("InvSwordsWindow");
        InvMolotsWindow = GameObject.Find("InvMolotsWindow");
        InvChestsWindow = GameObject.Find("InvChestsWindow");
        InvRingsWindow = GameObject.Find("InvRingsWindow");

        Equipment = GetComponent<Canvas>();
        Equipment.enabled = false;

        OI = GameObject.FindGameObjectWithTag("InvCanvas").GetComponent<OpenInventory>();
    }

    void Update()
    {
        if (OpenEquipmentCheck == false)
        {
            InvSwordsWindow.SetActive(true);
            InvMolotsWindow.SetActive(true);
            InvChestsWindow.SetActive(true);
            InvRingsWindow.SetActive(true);
        }
    }

    public void EquipOpenButtonClick()
    {
        Equipment.enabled = true;
        OI.canvas.enabled = false;
        OpenEquipmentCheck = true;

        InvSwordsWindow.SetActive(true);
        InvMolotsWindow.SetActive(false);
        InvChestsWindow.SetActive(false);
        InvRingsWindow.SetActive(false);
    }

    public void EquipExitButtonClick()
    {
        Equipment.enabled = false;
        OI.canvas.enabled = true;
        OpenEquipmentCheck = false;
    }

    public void OpenSwords()
    {
        InvSwordsWindow.SetActive(true);
        InvMolotsWindow.SetActive(false);
        InvChestsWindow.SetActive(false);
        InvRingsWindow.SetActive(false);
    }

    public void OpenMolots()
    {
        InvSwordsWindow.SetActive(false);
        InvMolotsWindow.SetActive(true);
        InvChestsWindow.SetActive(false);
        InvRingsWindow.SetActive(false);
    }

    public void OpenChests()
    {
        InvSwordsWindow.SetActive(false);
        InvMolotsWindow.SetActive(false);
        InvChestsWindow.SetActive(true);
        InvRingsWindow.SetActive(false);
    }

    public void OpenRings()
    {
        InvSwordsWindow.SetActive(false);
        InvMolotsWindow.SetActive(false);
        InvChestsWindow.SetActive(false);
        InvRingsWindow.SetActive(true);
    }
}
