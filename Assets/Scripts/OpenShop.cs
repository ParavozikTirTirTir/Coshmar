using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public bool trigger = false;
    public Canvas canvas;
    public bool OpenShopCheck = false;

    private GameObject HealBar;

    private OpenInventory OI;
    private OpenMagicBook MB;
    private IsPlayerInDialoge PinD;

    private bool State = true;

    void Start()
    {
        canvas = GameObject.Find("Shop").GetComponent<Canvas>();
        canvas.enabled = false;

        MB = GameObject.Find("MagicBook").GetComponent<OpenMagicBook>();
        OI = GameObject.Find("InventoryCanvas").GetComponent<OpenInventory>();
        HealBar = GameObject.Find("UI health");
    }


    void Update()
    {
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();

        if (Input.GetKeyDown(KeyCode.E) && (trigger == true || OpenShopCheck))
        {
            OpenInventory.PlayerCanMove = !OpenInventory.PlayerCanMove;
            OpenShopCheck = !OpenShopCheck;
            State = !State;
            HealBar.SetActive(State);
            canvas.enabled = !canvas.enabled;
            //PinD.InDialoge = !PinD.InDialoge;
        }

        //if (!OpenShopCheck && !MB.OpenBookCheck && !OI.OpenInventoryCheck && !PinD.InDialoge)
        //{
        //    OpenInventory.PlayerCanMove = true;
        //}
    }

    void OnGUI()
    {
        if (trigger && OpenInventory.PlayerCanMove)
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 110, 25), "[Е] Поговорить");
        }
    }

    void OnTriggerStay2D(Collider2D obj) //игрок рядом с НПС
    {
        if (obj.tag == "Player" && obj.GetComponent<PlayerController>().movementSpeed != 0)
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //игрок отошел от НПС
    {
        if (obj.tag == "Player")
        {
            trigger = false;
        }
    }
}
