using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class Craft : MonoBehaviour
{
    public GameObject[] Objects; // объекты, которые мы можем скрафтить (рецепты)

    public Object ItemForCraft;

    private Inventory Inv;
    private Instruments Inst;

    public TMP_Text RecipeText;
    private TMP_Text ToolInfoString;
    private TMP_Text ToolInfo;
    private Image SelectedTool;
    public Sprite PNGpicture;

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        Inst = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Instruments>();
        RecipeText = GameObject.Find("RecipeText").GetComponent<TMP_Text>();
        ToolInfoString = GameObject.Find("ToolInfoString").GetComponent<TMP_Text>();
        ToolInfo = GameObject.Find("ToolInfo").GetComponent<TMP_Text>();
        SelectedTool = GameObject.Find("SelectedTool").GetComponent<Image>();
    }

    void Update()
    {
        if (Inst.SelectedMolot.Name != "")
        {
            SelectedTool.sprite = Inst.SelectedMolot.Sprite;
            ToolInfoString.text = $"Эффективность\nПрочность";
            ToolInfo.text = $"{Inst.SelectedMolot.EnergyReduction}%\n{Inst.SelectedMolot.Durability}";
        }
        else
        {
            SelectedTool.sprite = PNGpicture;
            ToolInfoString.text = $"";
            ToolInfo.text = $"";
        }
    }

    public void CraftButtonClick()
    {
        string recipesInfo = "";

        foreach (var recipe in ItemForCraft.ItemRecipe)
        {
            if (recipe.ComponentName != "Энергия")
            {
                var itemInInventory = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
                int availableAmount = itemInInventory != null ? itemInInventory.Amount : 0;
                recipesInfo += $"{recipe.ComponentName}: {availableAmount}/{recipe.Amount}\n";
            }
            else
            {
                recipesInfo += $"{recipe.ComponentName}: {Inst.Energy}/{recipe.Amount}\n";
            }
        }

        if (HasComponents(ItemForCraft.ItemRecipe) && ItemForCraft.Name != "")
        {
            recipesInfo = "";

            for (int i = 0; i < Inv.ObjectsInInventory.Length; i++)
            {
                if (Inv.ObjectsInInventory[i].Amount == 0)
                {
                    Inv.ObjectsInInventory[i] = ItemForCraft.Clone();
                    break;
                }

                if (Inv.ObjectsInInventory[i].Name == ItemForCraft.Name)
                {
                    Inv.ObjectsInInventory[i].Amount += ItemForCraft.Amount;
                    break;
                }
            }

            foreach (var recipe in ItemForCraft.ItemRecipe)
            {
                var item = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
                if (item != null)
                {
                    item.Amount -= recipe.Amount; // уменьшение количество компонентов в инвентаре
                    if (item.Amount == 0)
                    {
                        item.Name = "";
                        item.Type = "";
                        item.Sprite = Inv.Sprites[4];
                        item.Attack = 0;
                        item.Durability = 0;
                        item.EnergyReduction = 0;
                    }
                }

                if (recipe.ComponentName != "Энергия")
                {
                    var itemInInventory = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
                    int availableAmount = itemInInventory != null ? itemInInventory.Amount : 0;
                    recipesInfo += $"{recipe.ComponentName}: {availableAmount}/{recipe.Amount}\n";
                }

                else
                {
                    Inst.Energy -= (float)Math.Floor(recipe.Amount*(1 - Inst.SelectedMolot.EnergyReduction/100));
                    recipesInfo += $"{recipe.ComponentName}: {Inst.Energy}/{recipe.Amount}\n";
                }
            }
            recipesInfo += "\nПредмет создан!";
        }
        else
        {
            if (ItemForCraft.Name != "")
            {
                recipesInfo += "\nНехватает материалов.";
            }
        }

        RecipeText.text = recipesInfo;
    }

    public bool HasComponents(Recipe[] recipes)
    {
        foreach (var recipe in recipes)
        {
            var item = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
            if ((item == null || item.Amount < recipe.Amount) && recipe.ComponentName != "Энергия")
            {
                return false; // Не хватает компонента
            }
            if (recipe.ComponentName == "Энергия" && recipe.Amount > Inst.Energy)
            {
                return false; // Не хватает энергии
            }
        }
        return true;
    }
}
