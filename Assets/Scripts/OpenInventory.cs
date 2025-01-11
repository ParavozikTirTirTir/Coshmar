using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class OpenInventory : MonoBehaviour
{
    public static bool PlayerCanMove;
    public bool PCM;
    public Canvas canvas;
    public bool OpenInventoryCheck = false;

    public GameObject HealBar;

    private PlayerController PC;
    private PlayerCombatController PCC;
    private IsPlayerInDialoge PinD;
    private OpenMagicBook MB;
    private OpenCraft OC;
    private OpenEquipment OE;
    private Instruments Inst;

    private bool State = true;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        MB = GameObject.Find("MagicBook").GetComponent<OpenMagicBook>();
        OC = GameObject.Find("Craft").GetComponent<OpenCraft>();
        OE = GameObject.Find("Equipment").GetComponent<OpenEquipment>();
        Inst = GameObject.Find("Inventory").GetComponent<Instruments>();
    }

    void Update()
	{
        PCM = PlayerCanMove;
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();

        if (Input.GetKeyDown(KeyCode.I) && !PinD.InDialoge && !MB.OpenBookCheck && !OC.OpenCraftCheck && !OE.OpenEquipmentCheck)
		{
            //DialogeState();
            PlayerCanMove = false;
            OpenInventoryCheck = !OpenInventoryCheck;
            State = !State;
            HealBar.SetActive(State);
            canvas.enabled = !canvas.enabled;
        }

        if (!OpenInventoryCheck && !MB.OpenBookCheck && !OC.OpenCraftCheck && !OE.OpenEquipmentCheck && !PinD.InDialoge)
        {
            PlayerCanMove = true;
            //DialogeExit();         
        }
        else
        {
            PlayerCanMove = false;
        }

        if (PlayerCanMove)
        {
            PC.movementSpeed = Inst.Speed;
            PC.jumpForce = Inst.JumpHeight;
            PC.dashSpeed = 20;
            PCC.combatEnabled = true;
        }
        else
        {
            PC.movementSpeed = 0;
            PC.jumpForce = 0;
            PC.dashSpeed = 0;
            PCC.combatEnabled = false;
        }
    }
}
