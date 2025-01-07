using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class OpenInventory : MonoBehaviour
{
	public Canvas canvas;
    public bool OpenInventoryCheck = false;

    public GameObject HealBar;

    private PlayerController PC;
    private PlayerCombatController PCC;
    private IsPlayerInDialoge PinD;
    private OpenMagicBook MB;
    private OpenCraft OC;

    private bool State = true;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();
        MB = GameObject.Find("MagicBook").GetComponent<OpenMagicBook>();
        OC = GameObject.Find("Craft").GetComponent<OpenCraft>();
    }

    void Update()
	{
		if (Input.GetKeyDown(KeyCode.I) && !PinD.InDialoge && !MB.OpenBookCheck && !OC.OpenCraftCheck)
		{
            DialogeState();
            OpenInventoryCheck = !OpenInventoryCheck;
            State = !State;
            HealBar.SetActive(State);
            canvas.enabled = !canvas.enabled;
        }

        if (!OpenInventoryCheck && !MB.OpenBookCheck && !OC.OpenCraftCheck)
        {  
             DialogeExit();         
        }
    }

    public void DialogeState()
    {
        PC.movementSpeed = 0;
        PC.jumpForce = 0;
        PC.dashSpeed = 0;
        PCC.combatEnabled = false;
    }

    public void DialogeExit()
    {
        PC.movementSpeed = 7;
        PC.jumpForce = 16;
        PC.dashSpeed = 20;
        PCC.combatEnabled = true;
    }
}
