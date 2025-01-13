using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenCraft : MonoBehaviour
{
    //public IsPlayerCanMove PlayerCanMove;
    public Canvas canvas;
    public bool OpenCraftCheck = false;

    public GameObject HealBar;

    private OpenInventory OI;
    private OpenMagicBook MB;
    private OpenShop OS;

    private IsPlayerInDialoge PinD;
    private bool State = true;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        MB = GameObject.Find("MagicBook").GetComponent<OpenMagicBook>();
        OI = GameObject.Find("InventoryCanvas").GetComponent<OpenInventory>();
        OS = GameObject.Find("GoblinShop").GetComponent<OpenShop>();
        HealBar = GameObject.Find("UI health");
    }


    void Update()
    {
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();

        if (Input.GetKeyDown(KeyCode.C) && !PinD.InDialoge && !MB.OpenBookCheck && !OI.OpenInventoryCheck && !OS.OpenShopCheck)
        {
            OpenInventory.PlayerCanMove = OpenInventory.PlayerCanMove;
            OpenCraftCheck = !OpenCraftCheck;
            State = !State;
            HealBar.SetActive(State);
            canvas.enabled = !canvas.enabled;
        }

        //if (!OpenCraftCheck && !MB.OpenBookCheck && !OI.OpenInventoryCheck && !PinD.InDialoge)
        //{
        //    OpenInventory.PlayerCanMove = true;
        //}
    }
}
