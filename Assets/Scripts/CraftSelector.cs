using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftSelector : MonoBehaviour
{
    public TMP_Text RecipeText;
    public TMP_Text SelectedName;
    public Object ObjectRecipe;
    public GameObject SlotSprite;
    public GameObject SelectedSlotSprite;
    public int RecipeIndex;

    private Craft CO;
    private Inventory Inv;

    public void OnButtonClick()
    {
        CO.ItemForCraft = ObjectRecipe;
        string recipesInfo = "";

        foreach (var recipe in ObjectRecipe.ItemRecipe)
        {
            var itemInInventory = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
            int availableAmount = itemInInventory != null ? itemInInventory.Amount : 0;
            recipesInfo += $"{recipe.ComponentName}: {availableAmount}/{recipe.Amount}\n";
        }

        RecipeText.text = recipesInfo;
        SelectedName.text = ObjectRecipe.Name;
        SelectedSlotSprite.GetComponent<Image>().sprite = ObjectRecipe.Sprite;
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
        ObjectRecipe = CO.Objects[RecipeIndex].GetComponent<ObjectWeapon>().Item;
        SlotSprite.GetComponent<Image>().sprite = ObjectRecipe.Sprite;
    }
}
