using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftSelector : MonoBehaviour
{
    public TMP_Text RecipeText;
    public GameObject Object;
    public GameObject SlotSprite;
    public GameObject SelectedSlotSprite;
    public int RecipeIndex;

    private Craft CO;

    public void OnButtonClick()
    {
        string recipesInfo = "";

        foreach (var recipe in Object.GetComponent<ObjectWeapon>().recipes)
        {
            recipesInfo += $"{recipe.ComponentName}: {recipe.Amount}\n";
        }

        RecipeText.text = recipesInfo;
        SelectedSlotSprite.GetComponent<Image>().sprite = Object.GetComponent<SpriteRenderer>().sprite;
    }

    void Start()
    {
        SelectedSlotSprite = GameObject.Find("SelectedObjectSprite");

        CO = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<Craft>();
    }

    void Update()
    {
        Object = CO.Objects[RecipeIndex];
        SlotSprite.GetComponent<Image>().sprite = Object.GetComponent<SpriteRenderer>().sprite;
    }
}
