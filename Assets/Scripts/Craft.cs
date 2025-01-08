using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Craft : MonoBehaviour
{
    public GameObject[] Objects; // объекты, которые мы можем скрафтить (рецепты)
    //public Image[] Icon; // слоты для рецептов

    public GameObject ItemForCraft;

    private Inventory Inv;
    public TMP_Text RecipeText;

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        RecipeText = GameObject.Find("RecipeText").GetComponent<TMP_Text>();
    }

    void Update()
    {

    }

    public void CraftButtonClick()
    {
        string recipesInfo = "";

        foreach (var recipe in ItemForCraft.GetComponent<ObjectWeapon>().recipes)
        {
            var itemInInventory = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
            int availableAmount = itemInInventory != null ? itemInInventory.Amount : 0;
            recipesInfo += $"{recipe.ComponentName}: {availableAmount}/{recipe.Amount}\n";
        }

        if (HasComponents(ItemForCraft.GetComponent<ObjectWeapon>().recipes))
        {
            recipesInfo = "";

            for (int i = 0; i < Inv.ObjectsInInventory.Length; i++)
            {
                if (Inv.ObjectsInInventory[i].Amount == 0)
                {
                    Inv.ObjectsInInventory[i].Name = ItemForCraft.GetComponent<ObjectWeapon>().ObjectName;
                    Inv.ObjectsInInventory[i].Sprite = ItemForCraft.GetComponent<SpriteRenderer>().sprite;
                    Inv.ObjectsInInventory[i].Attack = ItemForCraft.GetComponent<ObjectWeapon>().Attack;
                    Inv.ObjectsInInventory[i].Durability = ItemForCraft.GetComponent<ObjectWeapon>().Durability;
                    Inv.ObjectsInInventory[i].Amount = 1;
                    break;
                }

                if (Inv.ObjectsInInventory[i].Name == ItemForCraft.GetComponent<ObjectWeapon>().ObjectName)
                {
                    Inv.ObjectsInInventory[i].Amount += 1;
                    break;
                }
            }

            foreach (var recipe in ItemForCraft.GetComponent<ObjectWeapon>().recipes)
            {
                var item = System.Array.Find(Inv.ObjectsInInventory, obj => obj.Name == recipe.ComponentName);
                if (item != null)
                {
                    item.Amount -= recipe.Amount; // уменьшение количество компонентов в инвентаре
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
