using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMolot : MonoBehaviour
{
    private MissionManager MM;
    private OpenInventory OI;
    private OpenMagicBook MB;
    private OpenCraft OC;
    private Inventory Inv;

    public double EnergyReduction;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MB = GameObject.Find("MagicBook").GetComponent<OpenMagicBook>();
        OI = GameObject.Find("InventoryCanvas").GetComponent<OpenInventory>();
        OC = GameObject.Find("Craft").GetComponent<OpenCraft>();
    }

    void Update()
    {
        
    }
}
