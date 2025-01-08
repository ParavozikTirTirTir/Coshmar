using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftSelector : MonoBehaviour
{
    public TMP_Text RecipeText;
    public TMP_Text SelectedName;
    public GameObject Object;
    public GameObject SlotSprite;
    public GameObject SelectedSlotSprite;
    public int RecipeIndex;

    private Craft CO;
    private Inventory Inv;

    public void OnButtonClick()
    {
        CO.ItemForCraft = Object;
        string recipesInfo = "";

        foreach (var recipe in Object.GetComponent<ObjectWeapon>().recipes)
        {
            var itemInInventory = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
            int availableAmount = itemInInventory != null ? itemInInventory.Amount : 0;
            recipesInfo += $"{recipe.ComponentName}: {availableAmount}/{recipe.Amount}\n";
        }

        RecipeText.text = recipesInfo;
        SelectedName.text = Object.GetComponent<ObjectWeapon>().ObjectName.ToString();
        SelectedSlotSprite.GetComponent<Image>().sprite = Object.GetComponent<SpriteRenderer>().sprite;
    }

    void Start()
    {
        SelectedSlotSprite = GameObject.Find("SelectedObjectSprite");

        RecipeText = GameObject.Find("RecipeText").GetComponent<TMP_Text>();
        SelectedName = GameObject.Find("SelectedName").GetComponent<TMP_Text>();
        CO = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<Craft>();
        Inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        Object = CO.Objects[RecipeIndex];
        SlotSprite.GetComponent<Image>().sprite = Object.GetComponent<SpriteRenderer>().sprite;
    }
}
