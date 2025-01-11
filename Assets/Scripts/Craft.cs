using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Craft : MonoBehaviour
{
    public GameObject[] Objects; // объекты, которые мы можем скрафтить (рецепты)

    public Object ItemForCraft;

    private Inventory Inv;
    public TMP_Text RecipeText;

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        RecipeText = GameObject.Find("RecipeText").GetComponent<TMP_Text>();
    }

    public void CraftButtonClick()
    {
        string recipesInfo = "";

        foreach (var recipe in ItemForCraft.ItemRecipe)
        {
            var itemInInventory = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
            int availableAmount = itemInInventory != null ? itemInInventory.Amount : 0;
            recipesInfo += $"{recipe.ComponentName}: {availableAmount}/{recipe.Amount}\n";
        }

        if (HasComponents(ItemForCraft.ItemRecipe))
        {
            recipesInfo = "";

            for (int i = 0; i < Inv.ObjectsInInventory.Length; i++)
            {
                if (Inv.ObjectsInInventory[i].Amount == 0)
                {
                    Inv.ObjectsInInventory[i] = ItemForCraft;
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

                var itemInInventory = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
                int availableAmount = itemInInventory != null ? itemInInventory.Amount : 0;
                recipesInfo += $"{recipe.ComponentName}: {availableAmount}/{recipe.Amount}\n";
            }
            recipesInfo += "\nПредмет создан!";
        }
        else
        {
            recipesInfo += "\nНедостаточно компонентов для крафта.";
        }

        RecipeText.text = recipesInfo;
    }

    public bool HasComponents(Recipe[] recipes)
    {
        foreach (var recipe in recipes)
        {
            var item = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
            if (item == null || item.Amount < recipe.Amount)
            {
                return false; // Не хватает компонента
            }
        }
        return true;
    }
}
