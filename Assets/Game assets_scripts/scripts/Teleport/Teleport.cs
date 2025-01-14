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
    public bool IsPlayerInCave;
    public bool IsPlayerInIceCave;
    public string music;
    public  bool trigger;
    public GameObject Obj;

    public bool IsCaveTeleport;
    public bool IsIceCaveTeleport;
    public Canvas ExitCanvasButtonCave;
    public Canvas ExitCanvasButtonIceCave;

    public Sprite LocSignSprite; //спрайт таблички с названием локации куда телепортирует

    private MissionManager MM;
    private LocationSign LS;
    private OpenInventory OI;
    private Button ExitButtonInteractable;
    private Button ExitButtonInteractableIce;

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

        ExitCanvasButtonCave = GameObject.Find("ExitCanvasButton").GetComponent<Canvas>();
        ExitCanvasButtonCave.enabled = false;
        ExitCanvasButtonIceCave = GameObject.Find("ExitCanvasButtonIce").GetComponent<Canvas>();
        ExitCanvasButtonIceCave.enabled = false;

        ExitButtonInteractable = GameObject.Find("ExitCaveButton").GetComponent<Button>();
        ExitButtonInteractable.interactable = false;
        ExitButtonInteractableIce = GameObject.Find("ExitIceCaveButton").GetComponent<Button>();
        ExitButtonInteractableIce.interactable = false;
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

            if (IsCaveTeleport)
            {
                IsPlayerInCave = true;
                if (!OI.OpenInventoryCheck)
                {
                    ExitCanvasButtonCave.enabled = true;
                    ExitButtonInteractable.interactable = true;
                }
            }

            if (IsIceCaveTeleport)
            {
                IsPlayerInIceCave = true;
                if (!OI.OpenInventoryCheck)
                {
                    ExitCanvasButtonIceCave.enabled = true;
                    ExitButtonInteractableIce.interactable = true;
                }
            }
        }

        if (OI.OpenInventoryCheck)
        {
            ExitCanvasButtonCave.enabled = false;
            ExitCanvasButtonIceCave.enabled = false;
        }

        if (!OI.OpenInventoryCheck)
        {
            if (IsPlayerInCave)
            {
                ExitCanvasButtonCave.enabled = true;
            }

            if (IsPlayerInIceCave)
            {
                ExitCanvasButtonIceCave.enabled = true;
            }
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
        ExitCanvasButtonCave.enabled = false;
        ExitButtonInteractable.interactable = false;
        IsPlayerInCave = false;

        ExitCanvasButtonIceCave.enabled = false;
        ExitButtonInteractableIce.interactable = false;
        IsPlayerInIceCave = false;

        if (IsCaveTeleport || IsIceCaveTeleport)
        {
            Obj = GameObject.FindGameObjectWithTag("Player");
            Obj.transform.position = CaveEntrance.transform.position;
        }
    }
}
