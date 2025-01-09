using NUnit;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Teleport : MonoBehaviour
{
    public Transform point;
    public Transform CaveEntrance;
    public string music;
    public  bool trigger;
    public GameObject Obj;

    public bool IsCaveTeleport;
    public Canvas ExitCanvasButton;

    public Sprite LocSignSprite; //спрайт таблички с названием локации куда телепортирует

    private MissionManager MM;
    private LocationSign LS;
    private OpenInventory OI;

    void OnTriggerStay2D(Collider2D collision)
    {
        trigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        LS = GameObject.FindGameObjectWithTag("LocSign").GetComponent<LocationSign>();
        OI = GameObject.Find("InventoryCanvas").GetComponent<OpenInventory>();
        ExitCanvasButton = GameObject.Find("ExitCanvasButton").GetComponent<Canvas>();
        ExitCanvasButton.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // При нажатии на клавишу Е и если игрок рядом с порталом
        {
            Obj = GameObject.FindGameObjectWithTag("Player");
            Obj.transform.position = point.transform.position;
            AudioManager2.instance.PlayMusic(music);
            LS.LocSignSprite = LocSignSprite;
            LS.IsTeleportationCommited = true;

            if (IsCaveTeleport && !OI.OpenInventoryCheck)
            {
                ExitCanvasButton.enabled = true;
            }
        }

        if (OI.OpenInventoryCheck)
        {
            ExitCanvasButton.enabled = false;
        }
    }

    void OnGUI() //кнопка отправиться
    {
        if (trigger) //игрок наступил на объект
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 130, 25), "[E] Отправиться...");
        }
    }

    public void OnButtonClick()
    {
        ExitCanvasButton.enabled = false;

        if (IsCaveTeleport)
        {
            Obj.transform.position = CaveEntrance.transform.position;
        }
    }
}
